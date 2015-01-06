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
 * @ modification time : 2014/11/18
 * @ modification time : 2014/12/1
 *
 * @author Loli
 */
public class AddLove extends HttpServlet {

    @Override
    public void service(ServletRequest req, ServletResponse res)
            throws ServletException, IOException {

        String id = req.getParameter("id");
        String name = req.getParameter("name");
//        String type = req.getParameter("type");
//        String training_time = req.getParameter("time");

        // 插入前還要再加上是否已有相同的存在的檢查
        ResultSet rs = null;

        try {
            rs = new SQL().getData("SELECT id FROM love "
                    + "WHERE "
                    + "id='" + id + "' AND "
                    + "name='" + name + "';");

            if (!rs.next()) {
                String sql = "INSERT INTO love (id,name,time) "
                        + "VALUES('" + id + "', '"
                        + name + "', now());";
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
