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
 * @ modification time : AM 11:57 2014/11/18
 * @ modification time : 2014/12/1
 * 
 * @author Loli
 */
public class AddLessonCount extends HttpServlet {

    @Override
    public void service(ServletRequest req, ServletResponse res)
            throws ServletException, IOException {

        String name = req.getParameter("name");
//        String type = req.getParameter("type");
//        String training_time = req.getParameter("training_time");

//        String sql = "INSERT INTO lesson_diy (count) VALUES(c);";
        String sql = "UPDATE lesson_diy_count "
                + "SET count = count + 1 "
                + "WHERE name = '" + name + "';";

        try {
            new SQL().setData(sql);
        } catch (ClassNotFoundException | InstantiationException |
                IllegalAccessException | SQLException ex) {
        }
    }

}
