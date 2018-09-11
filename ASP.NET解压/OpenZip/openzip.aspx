<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="openzip.aspx.cs" Inherits="OpenZip.openzip" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>在线解压源码</title>
    <link href="https://cdn.bootcss.com/bootstrap/4.1.1/css/bootstrap.min.css" rel="stylesheet" />
    <script src="https://cdn.bootcss.com/jquery/3.3.1/jquery.min.js"></script>
</head>
<body style="padding: 40px 0">
    <div class="container-fluid">
        <h2 class="text-center">C#在线解压工具</h2>
        <br />
        <div class="card">
            <div class="card-body">
                  <form id="open" name="open" action="?Action=openzip" method="post">
                    <div class="form-row">
                        <div class="form-group col-md-12">
                            <label>文件所在路径</label>
                            <input name="filepath"  type="text" class="form-control" placeholder="C://" />
                        </div>

                        <div class="form-group col-md-12">
                            <label>将文件解压至</label>
                            <input name="openpath"  type="text" class="form-control" placeholder="C://" />
                        </div>
                    </div>
                    <button type="submit" class="btn btn-primary">解压</button>
                </form>
            </div>
        </div>
    </div>

</body>
</html>
