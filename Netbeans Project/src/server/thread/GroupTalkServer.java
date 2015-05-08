/*
 * Copyright © 2014 Chenghsi Inc. All rights reserved
 */
package server.thread;

import java.io.IOException;
import java.net.ServerSocket;
import static server.config.Config.SERVER_IS_RUN;
import static server.config.Config.TCP_GROUP_TALK_SERVER_PORT;

/**
 * @author Loli, Last modification time : 2014/11/12
 */
public class GroupTalkServer implements Runnable {

    @Override
    public void run() {
        ServerSocket ss = null;
        try {
            ss = new ServerSocket(TCP_GROUP_TALK_SERVER_PORT);
            System.out.println("伺服器已啟動... " + TCP_GROUP_TALK_SERVER_PORT);
        } catch (IOException ex) {
            System.out.println(ex);
        }

        while (SERVER_IS_RUN) {
            try {
                System.out.println("等待連線..." + TCP_GROUP_TALK_SERVER_PORT);
                new GroupTalkClient(ss.accept()).start();
            } catch (Exception e) {
                System.out.println(e);
            }
        }
    }

}
