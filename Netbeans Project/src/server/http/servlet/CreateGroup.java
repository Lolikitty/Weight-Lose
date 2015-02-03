/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package server.http.servlet;

import java.io.IOException;
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
public class CreateGroup extends HttpServlet {

    @Override
    public void service(ServletRequest req, ServletResponse res) throws ServletException, IOException {

        String id = req.getParameter("id");
        String ids = req.getParameter("ids");

        String sql;

        try {
            sql = "SELECT nextval('group_id_sequence');";

            String newID = null;
            try (ResultSet rs = new SQL().getData(sql)) {
                rs.next();
                newID = rs.getString(1);
            }

            sql = "INSERT INTO groups (create_user_id, group_id, time) VALUES ('" + id + "', " + newID + ", now());";

            sql += "INSERT INTO group_id (group_id, id) VALUES (" + newID + ", '" + id + "');";
            
            String[] idsa = ids.split(",");
            for (String idsa1 : idsa) {
                sql += "INSERT INTO group_id (group_id, id) VALUES (" + newID + ", '" + idsa1 + "');";
            }
            

            new SQL().setData(sql);

        } catch (ClassNotFoundException | InstantiationException | IllegalAccessException | SQLException ex) {
            System.out.println(ex);
        }

//        
//        System.out.println(sql);
    }

}
