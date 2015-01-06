/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package server.http.servlet;

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
 *
 * @author Loli
 */
public class AddFriend extends HttpServlet {

    @Override
    public void service(ServletRequest req, ServletResponse res) throws ServletException, IOException {
        try (PrintWriter out = res.getWriter()) {

            String id = req.getParameter("id");
            String friend_id = req.getParameter("friend_id");

            String sql = "SELECT id FROM user_information WHERE id = " + friend_id + ";";

            try (ResultSet rs = new SQL().getData(sql)) {
                if (!rs.next()) {
                    out.print("NoID");
                    return;
                }
            }

            sql = "SELECT id FROM user_friend WHERE id = " + id + " AND friend_id=" + friend_id + " OR friend_id=" + id + " AND id=" + friend_id + ";";

            try (ResultSet rs = new SQL().getData(sql)) {
                if (rs.next()) {
                    out.print("AlreadyFriends");
                    return;
                }
            }

            sql = "INSERT INTO user_friend (id, friend_id, is_friend) VALUES (" + id + ", " + friend_id + "" + ", false);";
            new SQL().setData(sql);
            out.print("Pass");

        } catch (ClassNotFoundException | InstantiationException | IllegalAccessException | SQLException ex) {
            System.out.println(ex);
        }
    }

}
