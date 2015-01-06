/*
 * Copyright © 2014 Chenghsi Inc. All rights reserved
 */
package server.http.servlet.delete;

import java.io.IOException;
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
 * @author Loli, Last modification time : 2014/11/11
 */
public class AddMuscleStatus extends HttpServlet {

    @Override
    public void service(ServletRequest req, ServletResponse res)
            throws ServletException, IOException {

        res.setContentType("text/html;charset=UTF-8");

        // Request value
        String id = req.getParameter("id");
        String type = req.getParameter("type");

        // Get now date : year, month, day
        Calendar cal = Calendar.getInstance();
        cal.setTime(new Date());
        int year = cal.get(Calendar.YEAR);
        int month = cal.get(Calendar.MONTH) + 1;
        int day = cal.get(Calendar.DAY_OF_MONTH);

        // SQL
        String sql = "SELECT s1,time"
                + " FROM muscle"
                + " WHERE id = '" + id + "' "
                + " AND extract(year from time) = " + year
                + " AND extract(month from time) = " + month
                + " AND extract(day from time) = " + day + ";";

        // Save to database
        ResultSet rs = null;

        try {
            rs = new SQL().getData(sql);

            if (rs.next()) {
                sql = "UPDATE muscle SET " + type + " = 20 WHERE id = '" + id + "' AND time = '" + rs.getString("time") + "';";
            } else {
                sql = "INSERT INTO muscle (id,s1,s2,s3,s4,s5,s6,s7,s8,s9,s10,s11,s12,s13,s14,s15,s16,s17, time) VALUES ("
                        + id + ",0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,now());";
            }
            new SQL().setData(sql);
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

        sql = "SELECT time FROM muscle WHERE id = '" + id + "' ORDER BY time DESC;";

        try {
            rs = new SQL().getData(sql);
            String time = "";
            if (rs.next()) {
                time = rs.getString("time");
            }
            if (!time.equals("")) {
                sql = "UPDATE muscle SET " + type + " = 20, time = now() WHERE id = '" + id + "' AND time = '" + time + "';";
                new SQL().setData(sql);
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

    }

}
