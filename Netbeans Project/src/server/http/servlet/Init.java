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
import java.util.HashSet;
import javax.servlet.ServletException;
import javax.servlet.ServletRequest;
import javax.servlet.ServletResponse;
import javax.servlet.http.HttpServlet;
import server.database.SQL;

/**
 *
 * @author Loli
 */
public class Init extends HttpServlet {

    @Override
    public void service(ServletRequest req, ServletResponse res) throws ServletException, IOException {
        res.setContentType("text/html;charset=UTF-8");
        String id = req.getParameter("id");
        String sID, sFriendID, temp;
        HashSet<String> IDs = new HashSet<>();

        try (ResultSet rs = new SQL().getData("SELECT id, friend_id from user_friend WHERE is_friend = true AND (id = " + id + " OR friend_id = " + id + ");")) {
            while (rs.next()) {
                sID = rs.getString("id");
                sFriendID = rs.getString("friend_id");
                if (id.equals(sFriendID)) {
                    temp = sID;
                    sID = sFriendID;
                    sFriendID = temp;
                }
                IDs.add(sFriendID);
            }
        } catch (Exception ex) {
            System.out.println("GetFriend : " + ex);
        }
        try {
            try (ResultSet rs = new SQL().getData("SELECT group_id FROM group_id WHERE id = '" + id + "';")) {
                while (rs.next()) {
                    String group_id = rs.getString(1);
                    try (ResultSet rs2 = new SQL().getData("SELECT id FROM group_id WHERE group_id = '" + group_id + "'")) {
                        while (rs2.next()) {
                            IDs.add(rs2.getString(1));
                        }
                    }
                }
            }
        } catch (ClassNotFoundException | InstantiationException | IllegalAccessException | SQLException ex) {
            System.out.println("GetGroup : " + ex);
        }
        String msg = "";
        for (String ids : IDs) {
            String name = null;
            try (ResultSet rs = new SQL().getData("SELECT name FROM user_information WHERE id=" + ids + ";")) {
                rs.next();
                name = rs.getString(1);
            } catch (Exception ex) {
                System.out.println("GetFriend : " + ex);
            }

            msg += ids + "," + name + ";";
        }
        msg = msg.substring(0, msg.length() - 1);

        try (PrintWriter out = res.getWriter()) {
            out.println(msg);
        }
    }

}
