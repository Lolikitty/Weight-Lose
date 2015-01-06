/*
 * Copyright © 2014 Chenghsi Inc. All rights reserved
 */
package server.http.servlet.delete;

import java.io.IOException;
import java.sql.SQLException;
import javax.servlet.ServletException;
import javax.servlet.ServletRequest;
import javax.servlet.ServletResponse;
import javax.servlet.http.HttpServlet;
import server.database.SQL;

/**
 * @ modification time : 2014/11/13
 * @ modification time : 2014/12/1
 *
 * @author Loli
 */
public class AddTrainingDIY extends HttpServlet {

    @Override
    public void service(ServletRequest req, ServletResponse res)
            throws ServletException, IOException {

        String id = req.getParameter("id");
        String name = req.getParameter("name");
        String[] data = req.getParameter("data").split(";");

//        String name = req.getParameter("name");
//        String type = req.getParameter("type");
//        String time = req.getParameter("time");
//
//        System.out.println(id + " , " + name + " , " + type + " , " + time);
//        
        String sql = "";
        // 下次要在寫入之前做檢查，否則名稱、屬性、時間會重複
        for (String data2 : data) {
            String[] data3 = data2.split(",");
            String type = data3[0];
            String time = data3[1];

            sql += "INSERT INTO lesson_diy "
                    + "(id, name, type, training_time,time) "
                    + "VALUES('"
                    + id + "', '"
                    + name + "', '"
                    + type + "', '"
                    + time
                    + "' , now());";

        }

        sql += "INSERT INTO lesson_diy_count (count,name) VALUES(0,'" + name + "');";

        try {
            new SQL().setData(sql);
        } catch (ClassNotFoundException | InstantiationException |
                IllegalAccessException | SQLException ex) {
            System.out.println(ex.getMessage());
        }

    }

}
