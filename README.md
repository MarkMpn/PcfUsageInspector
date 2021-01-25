# PCF Usage Inspector

By [Mark Carrington](https://markcarrington.dev/)

PCF Usage Inspector is an XrmToolBox tool to identify in bulk which PCF controls are in use on which
forms or views.

It highlights controls known to be deprecated to make it easy to update forms or views to use new alternatives,
and controls that are not being used for attributes they are expected for.

## The control list

The main page of the plugin shows a list of all the controls that are available. The list is sorted by the solution
the control is part of, then by name. You can use the controls at the top of the list to quickly filter the controls
to find the ones you're interested in.

As well as the solution and control name, you can also see every form or view that the control is used in to make it
easier to find instances of the control that may need to be updated.

![PCF Usage Inspector Main View](Screenshots/Main%20View.png)

## Deprecated Controls

A number of controls provided by Microsoft have been deprecated, and so should not be used for new work and may be
removed in the future. These controls are highlighted in the list. If a deprecated control is not being used it is
highlighted in gray, if it is being used it is highlighted in red.

If a deprecated control is in use, you can use the "Usages" column to find where it is being used and replace it with
another appropriate control.

## Expected Controls

You may have internal standards for what controls should be used, either for specific attributes or all attributes of
a particular type. For example, you may want to use the toggle control for all boolean attributes, or Data8's
[PredictiveAddress control](https://www.data-8.co.uk/data-validation/address-validation/predictiveaddress-dynamics/)
for the `account.address1_line` attribute.

You can manage these expectations using the "Edit Expected Controls" button on the main page.

![Expected Control Rules](Screenshots/Expected%20Controls.png)

Click the "Add" button to add a new rule. You can add a rule for

* a single attribute
* all attributes of a specific type
* all attributes that use a particular global option set

With the rule selected, choose which control should be used and then select the appropriate options for that type of rule.

Rules for expected controls are used across all environments so you can enforce a consistent approach in your use of
PCF controls for all the environments you manage.
