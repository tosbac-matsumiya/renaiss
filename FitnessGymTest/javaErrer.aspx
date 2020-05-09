<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="javaErrer.aspx.vb" Inherits="FitnessGymTest.javaErrer" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Javascript Enable Check - test</title>
</head>
<body>
    <div id="JSNG" style="width: 400px; text-align: left; border: 5px solid #ffaaaa; padding: 10px;">
        <p>JavaScript が無効化されています。</p>
    </div>

    <script>  document.getElementById("JSNG").style.display = "none";</script>

    <div id="JSOK" style="display:none; width: 400px; text-align: left; border: 5px solid #aaaaff; padding: 10px;">
        <p>JavaScript が有効になっています！</p>
    </div>

    <script>  document.getElementById("JSOK").style.display = "block";</script>
</body>
</html>
