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
 * Last modification time : 2014/11/18
 *
 * @author Loli
 */
public class GetLove extends HttpServlet {

    @Override
    public void service(ServletRequest req, ServletResponse res)
            throws ServletException, IOException {

        res.setContentType("text/html;charset=UTF-8");

        String id = req.getParameter("id");

        String sql = "SELECT name, type, training_time "
                + "FROM love "
                + "WHERE id='" + id + "' "
                + "ORDER BY time ASC;";

        String msg = "";

        ResultSet rs = null;

        try {
            rs = new SQL().getData(sql);

            while (rs.next()) {
                msg
                        += rs.getString("name") + ","
                        + rs.getString("type") + ","
                        + rs.getString("training_time") + ";";
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
