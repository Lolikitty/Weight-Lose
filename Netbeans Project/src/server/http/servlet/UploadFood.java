/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package server.http.servlet;

import java.io.IOException;
import java.io.UnsupportedEncodingException;
import java.net.URLDecoder;
import javax.servlet.ServletException;
import javax.servlet.annotation.MultipartConfig;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import javax.servlet.http.Part;
import static server.config.Config.DIR_SERVER_DATA;

@MultipartConfig(fileSizeThreshold = 1024 * 1024 * 2, // 2MB
        maxFileSize = 1024 * 1024 * 10, // 10MB
        maxRequestSize = 1024 * 1024 * 50)   // 50MB

/**
 *
 * @author Loli
 */
public class UploadFood extends HttpServlet {

    @Override
    protected void doPost(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
        String id = request.getParameter("id");
        for (Part p : request.getParts()) {
            try {
                String fileName = extractFileName(p);
                if (fileName == null) {
                    continue;
                }
                if (fileName.equals("Food.jsp")) {
                    p.write(DIR_SERVER_DATA + "\\" + id + "\\" + fileName);
                } else {
                    p.write(DIR_SERVER_DATA + "\\" + id + "\\Food\\" + fileName);
                }
            } catch (Exception e) {
                System.out.println(e);
            }
        }
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
                    return null;
                }
            }
        }
        return null;
    }
}
