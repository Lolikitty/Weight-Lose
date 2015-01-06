/*
 * Copyright © 2014 Chenghsi Inc. All rights reserved
 */
package server.http.servlet.delete;

import java.io.IOException;
import java.io.PrintWriter;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.text.SimpleDateFormat;
import javax.servlet.ServletException;
import javax.servlet.ServletRequest;
import javax.servlet.ServletResponse;
import javax.servlet.http.HttpServlet;
import server.database.SQL;

/**
 * @author Loli, Last modification time : AM 11:57 2014/11/10
 */
public class GetMuscleStatusList extends HttpServlet {

    @Override
    public void service(ServletRequest req, ServletResponse res)
            throws ServletException, IOException {

        res.setContentType("text/html;charset=UTF-8");

        // Request value
        String id = req.getParameter("id");

        String sql = "SELECT s1,s2,s3,s4,s5,s6,s7,s8,s9,"
                + "s10,s11,s12,s13,s14,s15,s16,s17,time"
                + " FROM muscle"
                + " WHERE id = '" + id + "' "
                + "AND time > now() - interval '9 days' ORDER BY time DESC;";

        String msg = "";

        ResultSet rs = null;

        try {
            rs = new SQL().getData(sql);

            while (rs.next()) {
                int[] array = new int[17];
                for (int i = 0; i < array.length; i++) {
                    array[i] = Integer.parseInt(rs.getString("s" + (i + 1)));
                    if (array[i] < -20) {
                        array[i] = -20;
                    }
                    msg += array[i] + (i == array.length - 1
                            ? ("," + new SimpleDateFormat("yyyy-MM-dd")
                            .format(rs.getDate("time")) + ";")
                            : ",");
                }
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
        out.println(msg);
        
    }

}
