/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package server.http.servlet;

import java.io.IOException;
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
public class DeleteFriend extends HttpServlet {

    @Override
    public void service(ServletRequest req, ServletResponse res) throws ServletException, IOException {
        String id = req.getParameter("id");
        String friend_id = req.getParameter("friend_id");
        String sql = "DELETE FROM user_friend WHERE id = " + id + " AND friend_id = " + friend_id + " OR id = " + friend_id + " AND friend_id = " + id + " ;";
        try {
            new SQL().setData(sql);
        } catch (Exception ex) {
            System.out.println(ex);
        }
    }

}
