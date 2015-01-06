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
public class GetWaitFriend extends HttpServlet {

    @Override
    public void service(ServletRequest req, ServletResponse res) throws ServletException, IOException {
        String id = req.getParameter("id");
        String sql = "SELECT id FROM user_friend WHERE friend_id = " + id + " AND is_friend = false";
        String msg = "";
        try (PrintWriter out = res.getWriter()) {
            try (ResultSet rs = new SQL().getData(sql)) {
                while (rs.next()) {
                    String id2 = rs.getString("id");
                    try (ResultSet rs2 = new SQL().getData("SELECT name FROM user_information WHERE id = " + id2 + ";")) {
                        rs2.next();
                        msg += id2 + "," + rs2.getString("name") + ";";
                    }
                }
                out.print(msg);
            } catch (Exception ex) {
                System.out.println(ex);
            }
        }
    }

}
