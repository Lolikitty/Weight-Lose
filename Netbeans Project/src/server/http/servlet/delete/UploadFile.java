package server.http.servlet.delete;

import java.io.IOException;
import javax.servlet.ServletException;
import javax.servlet.annotation.MultipartConfig;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import javax.servlet.http.Part;

/**
 *
 * @author Loli
 */
@MultipartConfig(fileSizeThreshold = 1024 * 1024 * 2, // 2MB
        maxFileSize = 1024 * 1024 * 10, // 10MB
        maxRequestSize = 1024 * 1024 * 50)   // 50MB

public class UploadFile extends HttpServlet {

    @Override
    protected void doPost(HttpServletRequest request,
            HttpServletResponse response) throws ServletException, IOException {

        System.out.println("ok");

//        for (Part p : request.getParts()) {
//            System.out.println(extractFileName(p));
//        }
        Part p = request.getPart("file");
//        String fileName = extractFileName(p);
        p.write("New.jpg");

//        System.out.println(extractFileName(p));
//        p.write("New.jpg");
        System.out.println("ok");

//        response.sendRedirect("/index.html");
    }

    private String extractFileName(Part part) {
        String contentDisp = part.getHeader("content-disposition");
        String[] items = contentDisp.split(";");
        for (String s : items) {
            if (s.trim().startsWith("filename")) {
                return s.substring(s.indexOf("=") + 2, s.length() - 1);
            }
        }
        return "";
    }

}
