/*
 * Copyright © 2014 Chenghsi Inc. All rights reserved
 */
package server.http.servlet.delete;

import java.io.IOException;
import java.sql.ResultSet;
import java.sql.SQLException;
import javax.servlet.ServletException;
import javax.servlet.ServletRequest;
import javax.servlet.ServletResponse;
import javax.servlet.http.HttpServlet;
import server.database.SQL;

/**
 * Last modification time : 2014/11/11
 *
 * @author Loli
 */
public class SignUp extends HttpServlet {

    @Override
    public void service(ServletRequest req, ServletResponse res)
            throws ServletException, IOException {

        String id = req.getParameter("id");
        String name = req.getParameter("name");

        String sql = "SELECT id FROM users WHERE id = '" + id + "';";

        ResultSet rs = null;

        try {
            rs = new SQL().getData(sql);

            if (!rs.next()) {
                sql = "INSERT INTO users(id, name, signup_time) "
                        + "VALUES('" + id + "','" + name + "', now());";

                new SQL().setData(sql);
            }
        } catch (ClassNotFoundException | InstantiationException |
                IllegalAccessException | SQLException ex) {
            System.out.println(ex);
        } finally {
            try {
                if (rs != null) {
                    rs.close();
                }
            } catch (Exception e) {
                System.out.println("RS : " + e);
            }
        }

    }
}
