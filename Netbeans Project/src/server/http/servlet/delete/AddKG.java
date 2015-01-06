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
 * Last modification time : AM 11:57 2014/11/10
 *
 * @author Loli
 */
public class AddKG extends HttpServlet {

    @Override
    public void service(ServletRequest req, ServletResponse res)
            throws ServletException, IOException {

        String id = req.getParameter("id");
        String kg = req.getParameter("kg");

        String sql = "INSERT INTO kg(id,kg,time) VALUES('" + id + "','" + kg + "',now());";
        
        try {
            new SQL().setData(sql);
        } catch (ClassNotFoundException | InstantiationException |
                IllegalAccessException | SQLException ex) {
        }
    }

}
