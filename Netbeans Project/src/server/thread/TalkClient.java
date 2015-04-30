package server.thread;

import java.io.BufferedReader;
import java.io.InputStreamReader;
import java.io.OutputStreamWriter;
import java.io.PrintWriter;
import java.net.Socket;
import java.sql.ResultSet;
import java.util.ArrayList;
import java.util.HashMap;
import static server.config.Config.SERVER_IS_RUN;
import server.database.SQL;

public class TalkClient extends Thread {

    Socket s;

    public TalkClient(Socket s) {
        this.s = s;
    }

    static HashMap<String, PrintWriter> map = new HashMap<String, PrintWriter>();

    @Override
    public void run() {
        BufferedReader r2 = null;
        PrintWriter w2 = null;
        String myID2 = null;

        try (BufferedReader r = new BufferedReader(new InputStreamReader(s.getInputStream()))) {
            try (PrintWriter w = new PrintWriter(new OutputStreamWriter(s.getOutputStream()))) {

                //===================================================================================
                // Init
                //===================================================================================
                r2 = r;
                w2 = w;

                String[] ins = r.readLine().split(",");
                String id = ins[0];
                String roomID = ins[1];
                myID2 = id;

                System.out.println("[ Enter ] id : " + id + "     Room ID : " + roomID);

                ArrayList<String> friendIDs = new ArrayList<String>();

                try {
                    try (ResultSet rs = new SQL().getData("SELECT id FROM group_id WHERE group_id ='" + roomID + "';")) {
                        while (rs.next()) {
                            friendIDs.add(rs.getString(1));
                        }
                    }
                } catch (Exception e) {
                    System.out.println(e);
                }

                // Wait -----------------------------------------------------------------------------
                if (!r.readLine().equals("<InitFinish>")) {
                    return;
                }
                //===================================================================================
                // History
                //===================================================================================
                try {
                    try (ResultSet rs = new SQL().getData("SELECT id, msg FROM group_msg WHERE group_id = '" + roomID + "' ORDER BY time ASC;")) {
                        while (rs.next()) {
                            w.println(rs.getString("id") + "_" + rs.getString("msg"));
                            w.flush();
                            Thread.sleep(100);
                            System.out.println(rs.getString("id") + "_" + rs.getString("msg"));
                        }
                    }
                } catch (Exception e) {
                    System.out.println(e);
                }

                //===================================================================================
                // Real Time
                //===================================================================================
                map.put(id, w);

                while (SERVER_IS_RUN) {
                    String msg = r.readLine();
                    if (msg.equals("exit") || msg == null) {
                        System.out.println("Exit");
                        break;
                    }
                    System.out.println(msg);
                    try {
                        new SQL().setData("INSERT INTO group_msg VALUES('" + roomID + "', '" + id + "', '" + msg + "', now());");
                    } catch (Exception e) {
                        System.out.println(e);
                    }
                    try {
                        for (String fid : friendIDs) {
                            if (!fid.equals(id)) {
                                if (map.get(fid) != null) {
                                    System.out.println("##### : " + fid);
                                    PrintWriter fw = map.get(fid);
                                    fw.println(id + "_" + msg);
                                    fw.flush();
                                }
                            }
                        }
                    } catch (Exception e) {
                        System.out.println(e);
                    }
                }

//                String[] enter = r.readLine().split(",");
//                String myID = enter[0];
//                String friendID = enter[1];
//
//                myID2 = myID;
//
//                map.put(myID, s);
//
//                // History
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
//                // Real Time
//                while (SERVER_IS_RUN) {
//                    try {
//                        String data = r.readLine().split(",")[0];
//                        Socket fs = map.get(myID);
//                        System.out.println(data);
//                        if (fs != null) {
//                            try (PrintWriter fw = new PrintWriter(new OutputStreamWriter(fs.getOutputStream()))) {
//                                fw.println(myID + "_" + friendID + "_" + data);
//                                sql = "INSERT INTO message VALUES (" + myID + ", " + friendID + ", " + data + ", now());";
//                                new SQL().setData(sql);
//                            }
//                        }
//                    } catch (ClassNotFoundException | InstantiationException | IllegalAccessException | SQLException ex) {
//                        System.out.println("Real Time : " + ex);
//                    }
//                }
            }
        } catch (NullPointerException ex) {
            System.out.println("Exit -----------------------------");
        } catch (Exception ex) {
            System.out.println("Other : " + ex);
        }

        try {
            map.remove(myID2);
            w2.println("Quit");
        } catch (Exception e) {
        }

        try {
            s.close();
        } catch (Exception e) {
            System.out.println(e);
        }
    }
}
