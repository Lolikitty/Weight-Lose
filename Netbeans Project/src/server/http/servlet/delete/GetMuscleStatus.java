/*
 * Copyright © 2014 Chenghsi Inc. All rights reserved
 */
package server.http.servlet.delete;

import java.io.IOException;
import java.io.PrintWriter;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.Calendar;
import java.util.Date;
import javax.servlet.ServletException;
import javax.servlet.ServletRequest;
import javax.servlet.ServletResponse;
import javax.servlet.http.HttpServlet;
import server.database.SQL;

/**
 * @author Loli, Last modification time : AM 11:57 2014/11/10
 */
public class GetMuscleStatus extends HttpServlet {

    @Override
    public void service(ServletRequest req, ServletResponse res)
            throws ServletException, IOException {

        res.setContentType("text/html;charset=UTF-8");

        // Request value
        String id = req.getParameter("id");

        // Get now date : year, month, day
        Calendar cal = Calendar.getInstance();
        cal.setTime(new Date());
        int year = cal.get(Calendar.YEAR);
        int month = cal.get(Calendar.MONTH) + 1;
        int day = cal.get(Calendar.DAY_OF_MONTH);

        // SQL
        String sql = "SELECT s1,s2,s3,s4,s5,s6,s7,s8,s9,s10,s11,s12,s13,s14,s15,s16,s17"
                + " FROM muscle"
                + " WHERE id = '" + id
                + "' AND extract(year from time) = " + year
                + " AND extract(month from time) = " + month
                + " AND extract(day from time) = " + day + ";";

        String msg = "";

        ResultSet rs = null;

        try {
            rs = new SQL().getData(sql);

            if (rs.next()) {
                int[] array = new int[17];
                for (int i = 0; i < array.length; i++) {
                    array[i] = Integer.parseInt(rs.getString("s" + (i + 1)));
                    if (array[i] < -20) {
                        array[i] = -20;
                    }
                    msg += array[i] + (i == array.length - 1 ? "" : ",");
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
