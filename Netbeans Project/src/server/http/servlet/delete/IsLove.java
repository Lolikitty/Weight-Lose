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
 * @ modification time : 2014/11/18
 * @ modification time : 2014/12/1
 *
 * @author Loli
 */
public class IsLove extends HttpServlet {

    @Override
    public void service(ServletRequest req, ServletResponse res)
            throws ServletException, IOException {

        res.setContentType("text/html;charset=UTF-8");

        String id = req.getParameter("id");
        String name = req.getParameter("name");
//        String type = req.getParameter("type");
//        String training_time = req.getParameter("time");

        String sql = "SELECT id "
                + "FROM love "
                + "WHERE id='" + id
                + "' AND name='" + name + "';";

        String msg = "false";

        ResultSet rs = null;

        try {
            rs = new SQL().getData(sql);

            if (rs.next()) {
                msg = "true";
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
        out.print(msg);
    }

}
