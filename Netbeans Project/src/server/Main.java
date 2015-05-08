package server;

/*
 * Copyright © 2014 Chenghsi Inc. All rights reserved
 */
import java.io.PrintWriter;
import java.sql.ResultSet;
import java.sql.SQLException;
import server.config.Config;
import server.database.SQL;
import server.frame.Frame;
import server.http.HttpServer;
import server.thread.TalkClient;

/**
 * Last modification time : 2014/11/10
 *
 * @author Loli
 */
public class Main {

    public static void main(String[] args) throws Exception {
        new Config();
        new Thread(new Frame()).start();
        new Thread(new HttpServer()).start();
//        System.out.println(Config.DIR_FOOD);

//        new Thread(() -> {
//            while (true) {
//
//                try {
//                    Thread.sleep(1000);
//                } catch (Exception ex) {
//                }
//
//                if (TalkClient.TALK_ROOM.isEmpty()) {
//                    continue;
//                }
//
//                System.out.print("目前在線 ID ：");
//
//                for (String key : TalkClient.TALK_ROOM.keySet()) {
//                    System.out.print(key + " , ");
//                }
//
//                System.out.println();
//
//            }
//        }).start();

        
        
        
//        try {
//
//            String sql = "SELECT * FROM user_id_count;";
//            ResultSet rs = new SQL().getData(sql);
//            if (rs.next()) {
//                int id = rs.getInt("count") + 1;
//                rs.close();
//                
//                System.out.println(id);
//                
////                sql
////                        = "UPDATE user_id_count SET count = " + id + ";"
////                        + "INSERT INTO user_information (id, name) VALUES (" + id + ", " + id + ");";
////                new SQL().setData(sql);
//
//            }
//        } catch (ClassNotFoundException | InstantiationException | IllegalAccessException | SQLException ex) {
//            System.out.println(ex);
//        }
        
        
        
        
        
        
        
    }

}
