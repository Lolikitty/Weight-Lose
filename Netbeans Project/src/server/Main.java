package server;

/*
 * Copyright © 2014 Chenghsi Inc. All rights reserved
 */


import server.config.Config;
import server.frame.Frame;
import server.http.HttpServer;

/**
 * Last modification time : 2014/11/10
 *
 * @author Loli
 */
public class Main {

    public static void main(String[] args) throws Exception {
        new Config();
        new Thread(new Frame()).start();
        new Thread(new HttpServer()).start();
//        System.out.println(Config.DIR_FOOD);
    }

}
