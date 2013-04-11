twitter.bootstrap.htmlhelper
============================

Twitter Bootstrap HtmlHelper as simple as this:

    @Html.TextBoxRowFor(t => t.Name)
  
will generate the following HTML:

    <div class="control-group">
        <label class="control-label" for="Name">Name</label>
        <div class="controls">
            <input id="Name" name="Name" type="text" />
        </div>
    </div>

Or if validation is enabled the following HTML dom is generated:

    <div class="control-group">
        <label class="control-label" for="Id">Id</label>
        <div class="controls">
            <input data-val="true" data-val-number="The field Id must be a number." 
            data-val-required="The Id field is required." id="Id" name="Id" type="text" value="0" />
        </div>
    </div>

It's always fun to write less for more and so far we support:

    @Html.TextBoxRowFor(...);
    @Html.DropDownRowFor(...);
    @Html.NavBar(...);
    @Html.Breadcrumbs(...);
