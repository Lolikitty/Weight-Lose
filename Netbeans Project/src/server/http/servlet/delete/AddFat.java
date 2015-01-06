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
 * Last modification time : 2014/11/20
 *
 * @author Loli
 */
public class AddFat  extends HttpServlet {

    @Override
    public void service(ServletRequest req, ServletResponse res)
            throws ServletException, IOException {

        String id = req.getParameter("id");
        String fat = req.getParameter("fat");

        String sql = "INSERT INTO fat (id, fat, time) VALUES('" + id + "','" + fat + "',now());";
        
        try {
            new SQL().setData(sql);
        } catch (ClassNotFoundException | InstantiationException |
                IllegalAccessException | SQLException ex) {
        }
    }

}
