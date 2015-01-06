/*
 * Copyright © 2014 Chenghsi Inc. All rights reserved
 */
package server.http.servlet.delete;

import java.io.IOException;
import java.sql.SQLException;
import javax.servlet.ServletException;
import javax.servlet.ServletRequest;
import javax.servlet.ServletResponse;
import javax.servlet.http.HttpServlet;
import server.database.SQL;

/**
 * Last modification time : 2014/11/11
 *
 * @author Loli
 */
public class AddUserInformation extends HttpServlet {

    @Override
    public void service(ServletRequest req, ServletResponse res)
            throws ServletException, IOException {
        
        String id = req.getParameter("id");
        String sex = req.getParameter("sex");
        String age = req.getParameter("age");
        String height = req.getParameter("height");
        String weight = req.getParameter("weight");
        String fat = req.getParameter("fat");

        String sql = "UPDATE users SET "
                + "sex = '" + sex + "', "
                + "age = '" + age + "', "
                + "height = '" + height + "', "
                + "weight = '" + weight + "', "
                + "fat = '" + fat + "', "
                + "last_modified_time = now() "
                + "WHERE id = '" + id + "';";

        try {
            new SQL().setData(sql);
        } catch (ClassNotFoundException | InstantiationException |
                IllegalAccessException | SQLException ex) {
        }

    }
}
