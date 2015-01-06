/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package server.http.servlet;

import java.io.IOException;
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
public class UserName extends HttpServlet {

    @Override
    public void service(ServletRequest req, ServletResponse res) throws ServletException, IOException {
        
        String id = req.getParameter("id");
        String name = req.getParameter("name");

        String sql = "UPDATE user_information SET name = '" + name + "'  WHERE id = " + id + ";";

        try {
            new SQL().setData(sql);
        } catch (ClassNotFoundException | InstantiationException | IllegalAccessException | SQLException ex) {
            System.out.println("ex");
        }
    }

}
