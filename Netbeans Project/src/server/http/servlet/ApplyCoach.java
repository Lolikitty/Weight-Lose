/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package server.http.servlet;

import java.io.IOException;
import java.io.PrintWriter;
import javax.servlet.ServletException;
import javax.servlet.ServletRequest;
import javax.servlet.ServletResponse;
import javax.servlet.http.HttpServlet;
import server.database.SQL;
/**
 *
 * @author user
 */
public class ApplyCoach extends HttpServlet {

    @Override
    public void service(ServletRequest req, ServletResponse res) throws ServletException, IOException {
        try (PrintWriter out = res.getWriter()) {
            
            String id = req.getParameter("id");
            String phone_l = req.getParameter("phone_l");
            String line_l = req.getParameter("line_l");
            String skype_l = req.getParameter("skype_l");
            String smallsin_l = req.getParameter("smallsin_l");
//            SimpleDateFormat nowdate = new java.text.SimpleDateFormat("yyyy-MM-dd HH:mm:ss"); 
         //   System.out.println("line_l = " + line_l);
            
            try {   
                String sql = "INSERT INTO teacher (id,phone,line,skype,smallsin,time) "
                        + "VALUES ('" + id + "'"
                        + ", '" + phone_l + "'"
                        + ", '" + line_l + "'"
                        + ", '" + skype_l + "'"
                        + ", '" + smallsin_l + "'"
                        + ", now()" 
                        + ");";
                new SQL().setData(sql);
            } catch (Exception e) {
                out.print(e.toString());
            }

            out.print("Upload Success");
        }
    }

}
