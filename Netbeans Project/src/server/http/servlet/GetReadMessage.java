/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package server.http.servlet;

import java.io.IOException;
import java.io.PrintWriter;
import java.sql.ResultSet;
import javax.servlet.ServletException;
import javax.servlet.ServletRequest;
import javax.servlet.ServletResponse;
import javax.servlet.http.HttpServlet;
import server.database.SQL;

/**
 *
 * @author Loli
 */
public class GetReadMessage extends HttpServlet {

    @Override
    public void service(ServletRequest req, ServletResponse res) throws ServletException, IOException {
        String id = req.getParameter("id");
        String friend_id = req.getParameter("friend_id");

        String sql = "SELECT read FROM message_read WHERE id = " + id + " AND friend_id = " + friend_id;

        try (PrintWriter out = res.getWriter()) {
            try (ResultSet rs = new SQL().getData(sql)) {
                if (rs.next()) {
                    out.print(rs.getBoolean("read") ? "True" : "False");
                } else {
                    out.print("No Data");
                }
            } catch (Exception ex) {
                System.out.println(ex);
            }
        }
    }

}
