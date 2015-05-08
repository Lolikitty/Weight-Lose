/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package server.thread;

import java.io.IOException;
import java.net.ServerSocket;
import static server.config.Config.SERVER_IS_RUN;
import static server.config.Config.TCP_TALK_SERVER_PORT;

/**
 *
 * @author Loli
 */
public class TalkServer implements Runnable {

    @Override
    public void run() {
        ServerSocket ss = null;
        try {
            ss = new ServerSocket(TCP_TALK_SERVER_PORT);
            System.out.println("伺服器已啟動... " + TCP_TALK_SERVER_PORT);
        } catch (IOException ex) {
            System.out.println(ex);
        }

        while (SERVER_IS_RUN) {
            try {
                System.out.println("等待連線..." + TCP_TALK_SERVER_PORT);
                new TalkClient(ss.accept()).start();
            } catch (Exception e) {
                System.out.println(e);
            }
        }
    }
}
