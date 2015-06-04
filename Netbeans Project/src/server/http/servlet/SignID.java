/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package server.http.servlet;

import java.io.File;
import java.io.IOException;
import java.sql.ResultSet;
import java.sql.SQLException;
import javax.servlet.ServletException;
import javax.servlet.http.HttpServlet;
import server.database.SQL;
import java.io.PrintWriter;
import java.util.Random;
import javax.servlet.ServletRequest;
import javax.servlet.ServletResponse;
import static server.config.Config.DIR_SERVER_DATA;
import static server.config.Config.DIR_FOOD;

/**
 *
 * @author Loli
 */
public class SignID extends HttpServlet {

    @Override
    public void service(ServletRequest req, ServletResponse res) throws ServletException, IOException {

        res.setContentType("text/html;charset=UTF-8");

        try (PrintWriter out = res.getWriter()) {

            String sql = "SELECT * FROM user_id_count;";
            ResultSet rs = new SQL().getData(sql);
            if (rs.next()) {
                
                Random r = new Random();
                
                int id1 =  rs.getInt("count") + 1;
                
                int id2 = (int)(r.nextFloat() * 100);
                
                int id = Integer.parseInt(""+id1+id2);
                
                rs.close();

                createDir(id);

                sql
                        = "UPDATE user_id_count SET count = " + id1 + ";"
                        + "INSERT INTO user_information (id, name) VALUES (" + id + ", " + id + ");";
                new SQL().setData(sql);

                out.print(id);
            }
        } catch (ClassNotFoundException | InstantiationException | IllegalAccessException | SQLException ex) {
            System.out.println(ex);
        }
    }

    void createDir(int id) {

        File dir = new File(DIR_SERVER_DATA + "/" + id + "/" + DIR_FOOD);
        if (!dir.exists()) {
            dir.mkdirs();
        }
    }
}
