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
import java.sql.SQLException;
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
                // Init
                //===================================================================================

//                while(true){
//                    System.out.println(r.readLine());
//                }
                String[] data = r.readLine().split(",");
                myID = data[0];
                friendID = data[1];                
                
                TALK_ROOM.put(myID, w);

//                String[] ins = r.readLine().split(",");
//                String id = ins[0];
//                String roomID = ins[1];
//                myID2 = id;
//
//                System.out.println("[ Enter ] id : " + id + "     Room ID : " + roomID);
//
////                // Wait -----------------------------------------------------------------------------
////                if (!r.readLine().equals("<InitFinish>")) {
////                    return;
////                }
//
//                String[] enter = r.readLine().split(",");
//                String myID = enter[0];
//                String friendID = enter[1];
//
//                myID2 = myID;
//
//                map.put(myID, s);
//
//                //===================================================================================
//                // History
//                //===================================================================================
//                String msg = r.readLine().split(",")[0];
//                String sql = "select id, friend_id, msg from message Where id = " + myID + " AND friend_id = " + friendID + " OR id=" + friendID + " AND friend_id=" + myID + " ORDER BY time ASC;";
//                try (ResultSet rs = new SQL().getData(sql)) {
//                    while (rs.next()) {
//                        w.println(rs.getString("id") + "_" + rs.getString("friend_id") + "_" + rs.getString("msg"));
//                    }
//                } catch (Exception e) {
//                    System.out.println(e);
//                }
//
//                System.out.println("okokokokokokok");
//
//                //===================================================================================
//                // Real Time
//                //===================================================================================
                while (SERVER_IS_RUN) {
//                    try {
                    String msg = r.readLine();
                    if (msg == null) {
                        break;
                    }
                    PrintWriter pw = TALK_ROOM.get(friendID);
//                    
                    if (pw != null) {
                        pw.println(msg);
                        pw.flush();
                        System.out.println(msg);

//                                String sql = "INSERT INTO message VALUES (" + myID + ", " + friendID + ", " + data + ", now());";
//                                new SQL().setData(sql);
                    }
//                    } catch (ClassNotFoundException | InstantiationException | IllegalAccessException | SQLException ex) {
//                        System.out.println("Real Time : " + ex);
//                    }
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
