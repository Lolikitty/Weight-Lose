<%@ page contentType="text/html;charset=Big5"%> 
<%@ page import="java.sql.*"%> 
<html> 
<body> 
<h1>教練申請紀錄</h1>
<table border="2" style=" background-color:#0F9;">
 <tr  style="color:#FFF" bgcolor="#3399CC">
<td>Id</td>
<td>Phone</td>
<td>Line</td>
<td>Skype</td>
<td>Wechat</td>
<td>Time</td>
</tr>

<%Class.forName("org.postgresql.Driver").newInstance(); 
String url ="jdbc:postgresql://localhost:5432/weight_loss_db";
//soft為你的資料庫名 
String user="postgres"; 
String password="a"; 
Connection conn= DriverManager.getConnection(url,user,password); 
Statement stmt=conn.createStatement(ResultSet.TYPE_SCROLL_SENSITIVE,ResultSet.CONCUR_UPDATABLE); 
String sql="select * from teacher;"; 
ResultSet rs=stmt.executeQuery(sql); 
while(rs.next()) {%> 
<tr>
<td><%=rs.getString("id")%> </td>
<td><%=rs.getString("phone")%> </td>
<td><%=rs.getString("line")%>  </td>
<td><%=rs.getString("skype")%> </td>
<td><%=rs.getString("smallsin")%> </td>
<td><%=rs.getString("time")%> </td>
<tr>
<br>
<%}%> 
<%rs.close(); 
stmt.close(); 
conn.close(); 
%> 

</table>
</body> 
</html>
