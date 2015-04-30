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
public class GetGroup extends HttpServlet {

    @Override
    public void service(ServletRequest req, ServletResponse res) throws ServletException, IOException {
        res.setContentType("text/html;charset=UTF-8");
        String id = req.getParameter("id");
        String m = "";
        try (PrintWriter out = res.getWriter()) {
            try {
                try (ResultSet rs = new SQL().getData("SELECT group_id FROM group_id WHERE id = '" + id + "';")) {
                    while (rs.next()) {
                        String group_id = rs.getString(1);
                        String sql = "SELECT id FROM group_id WHERE group_id = '" + group_id + "'";
                        try (ResultSet rs2 = new SQL().getData(sql)) {
                            m += group_id + ":";
                            String m2 = "";
                            while (rs2.next()) {
                                String ID = rs2.getString(1);
                                String name = "No Name";
                                try (ResultSet rs3 = new SQL().getData("SELECT name FROM user_information WHERE id = " + ID)) {
                                    rs3.next();
                                    name = rs3.getString(1);
                                }
                                m2 += ID + "_" + name + ",";
                            }
                            m2 = m2.substring(0, m2.length() - 1);
                            m += m2 + ";";
                        }
                    }
                }
                if (!m.equals("")) {
                    m = m.substring(0, m.length() - 1);
                }
                out.print(m);
            } catch (ClassNotFoundException | InstantiationException | IllegalAccessException | SQLException ex) {
                System.out.println("GetGroup : " + ex);
            }
        }
    }

}
