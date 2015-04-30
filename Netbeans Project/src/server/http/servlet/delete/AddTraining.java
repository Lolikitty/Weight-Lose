/*
 * Copyright © 2014 Chenghsi Inc. All rights reserved
 */
package server.http.servlet.delete;

import java.io.IOException;
import java.sql.ResultSet;
import java.sql.SQLException;
import javax.servlet.ServletException;
import javax.servlet.ServletRequest;
import javax.servlet.ServletResponse;
import javax.servlet.http.HttpServlet;
import server.database.SQL;

/**
 * @ modification time : 2014/11/10
 * @ modification time : 2014/12/1
 *
 * @author Loli
 */
public class AddTraining extends HttpServlet {

    @Override
    public void service(ServletRequest req, ServletResponse res)
            throws ServletException, IOException {

        String id = req.getParameter("id");
        String p1 = req.getParameter("p1");
        String p2 = req.getParameter("p2");
        String p3 = req.getParameter("p3");
        String kg = req.getParameter("kg");
        String count = req.getParameter("count");

        //-------------------------------------------------------
        String sql = "SELECT id FROM exp WHERE id='"+id+"';";

        ResultSet rs = null;

        try {
            rs = new SQL().getData(sql);

            if (!rs.next()) {
                sql = "INSERT INTO exp (id, exp) "
                        + "VALUES('" + id + "',0);";
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
        //-------------------------------------------------------

        sql = "INSERT INTO training(id,p1,p2,p3,kg,count,time) "
                + "VALUES('" + id + "','" + p1 + "','" + p2 + "','" + p3 + "','"
                + kg + "','" + count + "',now());"
                + "UPDATE exp SET exp = exp + 25 WHERE id = '" + id + "';";

        try {
            new SQL().setData(sql);
        } catch (ClassNotFoundException | InstantiationException |
                IllegalAccessException | SQLException ex) {
            System.out.println(ex);
        }
    }
}
