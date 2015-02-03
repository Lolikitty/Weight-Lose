/*
 * Copyright © 2014 Chenghsi Inc. All rights reserved
 */
package server.http.servlet.delete;

import java.io.IOException;
import java.io.PrintWriter;
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
public class GetUserInformation extends HttpServlet {

    @Override
    public void service(ServletRequest req, ServletResponse res)
            throws ServletException, IOException {

        res.setContentType("text/html;charset=UTF-8");

        String id = req.getParameter("id");

        String sql = "SELECT sex, age, height, weight, fat "
                + "FROM users "
                + "WHERE id='" + id + "';";

        String msg = "";

        ResultSet rs = null;

        try {
            rs = new SQL().getData(sql);

            if (rs.next()) {
                msg = rs.getString("sex") + ","
                        + rs.getString("age") + ","
                        + rs.getString("height") + ","
                        + rs.getString("weight") + ","
                        + rs.getString("fat") + ",";
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

        PrintWriter out = res.getWriter();
        out.println(msg);
    }

}
