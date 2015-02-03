/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package server.http.servlet;

import java.io.IOException;
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
public class AgreeFriend extends HttpServlet {

    @Override
    public void service(ServletRequest req, ServletResponse res) throws ServletException, IOException {

        String id = req.getParameter("id");
        String friend_id = req.getParameter("friend_id");
        String agree = req.getParameter("agree");

        String sql = "";

        if (agree.equals("y")) {
            sql = "UPDATE user_friend SET is_friend = true WHERE id = " + friend_id + " AND friend_id = " + id + ";";
        } else {
            sql = "DELETE FROM user_friend  WHERE id = " + friend_id + " AND friend_id = " + id + ";";
        }
        try {
            new SQL().setData(sql);
        } catch (ClassNotFoundException | InstantiationException | IllegalAccessException | SQLException ex) {
            System.out.println(ex);
        }
    }
}
