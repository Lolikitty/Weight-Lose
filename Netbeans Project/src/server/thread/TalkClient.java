/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package server.thread;

import java.io.BufferedReader;
import java.io.InputStreamReader;
import java.io.OutputStreamWriter;
import java.io.PrintWriter;
import java.net.Socket;
import java.sql.ResultSet;
import java.util.HashMap;
import static server.config.Config.SERVER_IS_RUN;
import server.database.SQL;

/**
 *
 * @author Loli
 */
public class TalkClient extends Thread {

    Socket s;

    public TalkClient(Socket s) {
        this.s = s;
    }

    public static HashMap<String, PrintWriter> TALK_ROOM = new HashMap<>();

    @Override
    public void run() {
        String myID = null;
        String friendID = null;

        try (BufferedReader r = new BufferedReader(new InputStreamReader(s.getInputStream()))) {
            try (PrintWriter w = new PrintWriter(new OutputStreamWriter(s.getOutputStream()))) {

                //===================================================================================
                // Init Value
                //===================================================================================
                String[] data = r.readLine().split(",");
                myID = data[0];
                friendID = data[1];

                TALK_ROOM.put(myID, w);

                try {
                    Thread.sleep(1000);
                } catch (Exception e) {
                }

                //===================================================================================
                // Init Database
                //===================================================================================
                try (ResultSet rs = new SQL().getData("SELECT id FROM message_read WHERE id = " + friendID + " AND friend_id = " + myID + ";")) {
                    if (!rs.next()) {
                        new SQL().setData("INSERT INTO message_read VALUES (" + friendID + ", " + myID + ", true);");
                    }
                }
                
                new SQL().setData("UPDATE message_read SET read = true" + " WHERE id = " + myID + " AND friend_id = " + friendID + ";");

                //===================================================================================
                // History
                //===================================================================================
                String sql = "select id, friend_id, msg from message Where id = " + myID + " AND friend_id = " + friendID + " OR id=" + friendID + " AND friend_id=" + myID + " ORDER BY time ASC;";

                try (ResultSet rs = new SQL().getData(sql)) {
                    while (rs.next()) {
                        w.println(rs.getString("id") + "_" + rs.getString("friend_id") + "_" + rs.getString("msg"));
                        w.flush();
                        try {
                            Thread.sleep(200);
                        } catch (Exception e) {
                        }
                    }
                } catch (Exception e) {
                    System.err.println("TalkClient.java -> Read DataBase Error : " + e);
                }

                //===================================================================================
                // Real Time
                //===================================================================================
                while (SERVER_IS_RUN) {

                    // Get Message
                    String msg2 = r.readLine();

                    // Check Message
                    if (msg2 == null) {
                        break;
                    }

                    boolean read = false;

                    // Realtime Message To Friend
                    PrintWriter pw = TALK_ROOM.get(friendID);
                    if (pw != null) {
                        pw.println(myID + "_" + friendID + "_" + msg2);
                        pw.flush();
                        read = true;
                    }

                    // Save Message Into Database
                    try {
                        new SQL().setData("INSERT INTO message VALUES (" + myID + ", " + friendID + ", '" + msg2 + "' , now());");
                    } catch (Exception e) {
                        System.err.println("TalkClient.java -> Set DataBase Error : " + e);
                    }
                    new SQL().setData("UPDATE message_read SET read = " + read + " WHERE id = " + friendID + " AND friend_id = " + myID + ";");
                }
            }
        } catch (NullPointerException ex) {
            System.out.println("Exit -----------------------------");
        } catch (Exception ex) {
            System.out.println("Other : " + ex);
        }

        try {
            TALK_ROOM.remove(myID);
        } catch (Exception e) {
        }

        try {
            s.close();
        } catch (Exception e) {
            System.out.println(e);
        }
    }

}
