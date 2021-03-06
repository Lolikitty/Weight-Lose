/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package server.http.servlet;

import java.io.File;
import java.io.IOException;
import java.io.PrintWriter;
import java.util.ArrayList;
import javax.servlet.ServletException;
import javax.servlet.ServletRequest;
import javax.servlet.ServletResponse;
import javax.servlet.http.HttpServlet;
import server.config.Config;
import static server.config.Config.DIR_SERVER_DATA;
import static server.config.Config.SERVER_PATH;

/**
 *
 * @author Loli
 */
public class ReadBackup extends HttpServlet {

    @Override
    public void service(ServletRequest req, ServletResponse res) throws ServletException, IOException {
        res.setContentType("text/html;charset=UTF-8");
        try (PrintWriter out = res.getWriter()) {
            String id = req.getParameter("id");
            ArrayList<String> a = getFilePath(SERVER_PATH + "\\" + DIR_SERVER_DATA + "\\Facebook\\" + id);
            String msg = "";
            for (String path : a) {
                msg += path + ",";
            }
            out.print(msg.substring(0, msg.length() - 1));
        }
    }

//    ArrayList<String> getFilePathList(){
//    
//    }
//    ArrayList<String> a = new ArrayList<String>();
    ArrayList<String> getFilePath(String path) {
        try {
            ArrayList<String> a = new ArrayList<>();

            for (String p : new File(path).list()) {
                String sp = path + "\\" + p;
                if (new File(sp).isDirectory()) {
                    System.out.println(getFilePath(sp));
                    ArrayList<String> aa = getFilePath(sp);
                    for (String data : aa) {
                        a.add(data);
                    }
                } else {
                    String [] s = Config.SERVER_PATH.replaceAll("\\\\", "/").split("/");
                    
                  a.add(sp.split(s[s.length-1])[1].replaceAll("\\\\", "/"));             
                }
            }
            return a;
        } catch (Exception e) {
            System.out.println(e);
        }
        return null;
    }

}
