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
public class GetFriend extends HttpServlet {

    @Override
    public void service(ServletRequest req, ServletResponse res) throws ServletException, IOException {
        res.setContentType("text/html;charset=UTF-8");
        
        String id = req.getParameter("id");
        String sql = "SELECT id, friend_id from user_friend WHERE is_friend = true AND (id = " + id + " OR friend_id = " + id + ");";
        String msg = "";

        String sID, sFriendID, sName, temp;

        try (PrintWriter out = res.getWriter()) {
            try (ResultSet rs = new SQL().getData(sql)) {
                while (rs.next()) {
                    sID = rs.getString("id");
                    sFriendID = rs.getString("friend_id");
                    if (id.equals(sFriendID)) {
                        temp = sID;
                        sID = sFriendID;
                        sFriendID = temp;
                    }
                    sql = "SELECT name FROM user_information WHERE id = " + sFriendID + ";";
                    try (ResultSet rs2 = new SQL().getData(sql)) {
                        rs2.next();
                        sName = rs2.getString("name");
                        msg += sFriendID + "," + sName + ";";
                    }
                }
                out.print(msg);
            } catch (Exception ex) {
                System.out.println("GetFriend : " + ex);
            }
        }

    }

}
