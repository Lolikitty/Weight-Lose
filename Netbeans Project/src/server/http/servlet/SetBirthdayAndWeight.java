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
public class SetBirthdayAndWeight extends HttpServlet {

    @Override
    public void service(ServletRequest req, ServletResponse res) throws ServletException, IOException {
        
        String id = req.getParameter("id");
        String sex = req.getParameter("sex");
        String year = req.getParameter("year");
        String month = req.getParameter("month");
        String day = req.getParameter("day");
        String weight_first = req.getParameter("weight_first");
        String weight_target = req.getParameter("weight_target");
        String weight_target_month = req.getParameter("weight_target_month");

        String sql = "UPDATE user_information SET " + "sex = '" + sex + "', " + "birthday_year = " + year + ", " + "birthday_month = " + month + ", " + "birthday_day = " + day + ", weight_now = " + weight_first + ", weight_target = " + weight_target + ", weight_target_month = " + weight_target_month + " WHERE id = " + id + ";";

        try {
            new SQL().setData(sql);
        } catch (ClassNotFoundException | InstantiationException | IllegalAccessException | SQLException ex) {
            System.out.println("ex");
        }

    }

}
