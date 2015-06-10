@model SqlGIS.Models.LoginViewModel

@{
    ViewBag.Title = "Admin: Log In";
    Layout = "~/Views/Shared/_AdminLayout.cshtml";
}

<div class="panel">
    <div class="panel-heading">
        <h1>Log In</h1>
    </div>
    <div class="panel-body">
        <p class="lead">Please log in to access the administrative area:</p>
        @using (Html.BeginForm())
        {
            @Html.ValidationSummary()
            <div class="form-group">
                @Html.EditorForModel(new { @class="form-control"})
            </div>
            <input type="submit" value="Войти" class="btn btn-primary" />
        }
    </div>
</div>
