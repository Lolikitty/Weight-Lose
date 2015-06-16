/*
 * Copyright © 2014 Chenghsi Inc. All rights reserved
 */
package server.http;

import javax.servlet.MultipartConfigElement;
import org.eclipse.jetty.server.Handler;
import org.eclipse.jetty.server.Server;
import org.eclipse.jetty.server.handler.HandlerList;
import org.eclipse.jetty.server.handler.ResourceHandler;
import org.eclipse.jetty.servlet.ServletContextHandler;
import org.eclipse.jetty.servlet.ServletHolder;
import org.eclipse.jetty.webapp.WebAppContext;
import server.config.Config;
import server.http.servlet.AddFriend;
import server.http.servlet.AgreeFriend;
import server.http.servlet.ApplyCoach;
import server.http.servlet.Backup;
import server.http.servlet.CreateGroup;
import server.http.servlet.DeleteFriend;
import server.http.servlet.ReadBackup;
import server.http.servlet.GetFriend;
import server.http.servlet.GetGroup;
import server.http.servlet.GetReadMessage;
import server.http.servlet.GetWaitFriend;
import server.http.servlet.Init;
import server.http.servlet.SetBirthdayAndWeight;
import server.http.servlet.SignID;
import server.http.servlet.UploadFood;
import server.http.servlet.UserImage;
import server.http.servlet.UserName;

/**
 * Last modification time : 2014/11/10
 *
 * @author Loli
 */
public class HttpServer implements Runnable {

    public static Server SERVER = new Server(Config.HTTP_SERVER_PORT);

    @Override
    public void run() {
        WebAppContext wac = new WebAppContext();
        wac.setResourceBase(".");

        ServletContextHandler context = wac;

        ServletHolder sh = new ServletHolder(new UserImage());
        sh.getRegistration().setMultipartConfig(new MultipartConfigElement(Config.SERVER_PATH));
        context.addServlet(sh, "/UserImage");

        ServletHolder sh2 = new ServletHolder(new Backup());
        sh2.getRegistration().setMultipartConfig(new MultipartConfigElement(Config.SERVER_PATH));
        context.addServlet(sh2, "/Backup");

        ServletHolder sh3 = new ServletHolder(new UploadFood());
        sh3.getRegistration().setMultipartConfig(new MultipartConfigElement(Config.SERVER_PATH));
        context.addServlet(sh3, "/UploadFood");

        context.addServlet(new ServletHolder(new Init()), "/Init");
        context.addServlet(new ServletHolder(new SignID()), "/SignID");
        context.addServlet(new ServletHolder(new SetBirthdayAndWeight()), "/SetBirthdayAndWeight");
        context.addServlet(new ServletHolder(new UserName()), "/UserName");
        context.addServlet(new ServletHolder(new AddFriend()), "/AddFriend");
        context.addServlet(new ServletHolder(new GetWaitFriend()), "/GetWaitFriend");
        context.addServlet(new ServletHolder(new AgreeFriend()), "/AgreeFriend");
        context.addServlet(new ServletHolder(new GetFriend()), "/GetFriend");
        context.addServlet(new ServletHolder(new ReadBackup()), "/ReadBackup");
        context.addServlet(new ServletHolder(new CreateGroup()), "/CreateGroup");
        context.addServlet(new ServletHolder(new GetGroup()), "/GetGroup");
        context.addServlet(new ServletHolder(new DeleteFriend()), "/DeleteFriend");
        context.addServlet(new ServletHolder(new ApplyCoach()), "/ApplyCoach");
        context.addServlet(new ServletHolder(new GetReadMessage()), "/GetReadMessage");

//        Handler[] h = {context};
//        HandlerList hl = new HandlerList();
//        hl.setHandlers(h);
        SERVER.setHandler(context);
    }

}
