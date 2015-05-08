/*
 * Copyright © 2014 Chenghsi Inc. All rights reserved
 */
package server.frame;

import server.http.HttpServer;
import java.awt.Container;
import java.awt.Desktop;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.io.IOException;
import java.net.InetAddress;
import java.net.URISyntaxException;
import java.net.URL;
import java.net.URLEncoder;
import java.net.UnknownHostException;
import javax.swing.JButton;
import javax.swing.JFrame;
import javax.swing.JLabel;
import server.config.Config;
import server.thread.GroupTalkServer;
import server.thread.TalkServer;

/**
 * @author Loli, Last modification time : AM 11:57 2014/11/10
 */
public class Frame implements Runnable {

    Container cp;
    JLabel labelServerStatus;

    @Override
    public void run() {
        FrameInit();
        LabelServerPublicIP();
        LabelServerIP();
        LabelServerPath();
        LabelServerStatus();
        ButtonServerStart();
        ButtonServerStop();
        ButtonOpenWeb();
        ButtonOpenPath();
    }

    void FrameInit() {
        JFrame f = new JFrame("減肥 App 伺服器");
        f.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
        f.setSize(450, 300);
        f.setVisible(true);
        cp = f.getContentPane();
        cp.setLayout(null);
    }

    void LabelServerPublicIP() {
        JLabel l = new JLabel("公開 IP：    " + Config.HTTP_SERVER_IP + " : " + Config.HTTP_SERVER_PORT);
        l.setBounds(5, 0, 1000, 30);
        cp.add(l);
    }

    void LabelServerIP() {
        String ip = "";
        try {
            ip = InetAddress.getLocalHost().getHostAddress();
        } catch (UnknownHostException ex) {
        }
        JLabel l = new JLabel("內部 IP：    " + ip + " : " + Config.HTTP_SERVER_PORT);
        l.setBounds(5, 20, 1000, 30);
        cp.add(l);
    }

    void LabelServerPath() {
        JLabel l = new JLabel("伺服器路徑：" + Config.SERVER_PATH);
        l.setBounds(5, 40, 1000, 30);
        cp.add(l);
    }

    void LabelServerStatus() {
        labelServerStatus = new JLabel("伺服器狀態：已停止");
        labelServerStatus.setBounds(5, 60, 1000, 30);
        cp.add(labelServerStatus);
    }

    void ButtonServerStart() {
        JButton b = new JButton("啟動");
        b.setBounds(5, 100, 100, 30);
        cp.add(b);

        b.addActionListener(new ActionListener() {
            @Override
            public void actionPerformed(ActionEvent arg0) {
                new Thread(new Runnable() {
                    @Override
                    public void run() {
                        try {
                            HttpServer.SERVER.start();
                            labelServerStatus.setText("伺服器狀態：已啟動");
                            Config.SERVER_IS_RUN = true;
                            new Thread(new GroupTalkServer()).start();
                            new Thread(new TalkServer()).start();
                            HttpServer.SERVER.join();
                        } catch (Exception ex) {
                        }
                    }
                }).start();
            }
        });
    }

    void ButtonServerStop() {
        JButton b = new JButton("停止");
        b.setBounds(150, 100, 100, 30);
        cp.add(b);

        b.addActionListener(new ActionListener() {
            @Override
            public void actionPerformed(ActionEvent arg0) {
                new Thread(new Runnable() {
                    @Override
                    public void run() {
                        try {
                            HttpServer.SERVER.stop();
                            labelServerStatus.setText("伺服器狀態：已停止");
                            Config.SERVER_IS_RUN = false;
                        } catch (Exception ex) {
                        }
                    }
                }).start();
            }
        });
    }

    void ButtonOpenWeb() {
        JButton b = new JButton("打開伺服器網頁");
        b.setBounds(5, 200, 150, 30);
        cp.add(b);
        b.addActionListener(new ActionListener() {
            @Override
            public void actionPerformed(ActionEvent arg0) {
                try {
                    Desktop.getDesktop().browse(new URL("http://" + Config.HTTP_SERVER_IP + ":" + Config.HTTP_SERVER_PORT).toURI());
                } catch (URISyntaxException | IOException ex) {
                }
            }
        });
    }

    void ButtonOpenPath() {
        JButton b = new JButton("打開伺服器路徑");
        b.setBounds(200, 200, 150, 30);
        cp.add(b);
        b.addActionListener(new ActionListener() {
            @Override
            public void actionPerformed(ActionEvent arg0) {
                try {
                    String oPath = Config.SERVER_PATH;
                    Desktop.getDesktop().browse(new URL("file://" + oPath).toURI());
                } catch (URISyntaxException | IOException ex) {
                    System.out.println(ex);
                }
            }
        });
    }

}
