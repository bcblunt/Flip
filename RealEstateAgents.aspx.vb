﻿
Partial Class RealEstateAgents
    Inherits System.Web.UI.Page

    Protected Sub imgBanner_Click(sender As Object, e As ImageClickEventArgs) Handles imgBanner.Click
        Me.Page.Response.Redirect("~\FlipsRealEstate.aspx")
    End Sub

    Protected Sub btnInsertAgent_Click(sender As Object, e As EventArgs) Handles btnInsertAgent.Click
        Me.btnInsertAgent.Visible = False
        Me.dvAgents.Visible = True
    End Sub

    Protected Sub CancelAgentInsert(sender As Object, e As EventArgs)
        Me.btnInsertAgent.Visible = True
        Me.dvAgents.Visible = False
    End Sub

    Protected Sub dvAgents_ItemInserting(sender As Object, e As DetailsViewInsertEventArgs) Handles dvAgents.ItemInserting
        'Me.dsAgents.InsertParameters.Item("@AgentPic").DefaultValue =
        'Me.dsAgents.InsertParameters.Item("@First_Name").DefaultValue =
        'Me.dsAgents.InsertParameters.Item("@Last_Name").DefaultValue =
        'Me.dsAgents.InsertParameters.Item("@Address").DefaultValue =
        'Me.dsAgents.InsertParameters.Item("@City").DefaultValue =
        'Me.dsAgents.InsertParameters.Item("@Phone_Number").DefaultValue =
        'Me.dsAgents.InsertParameters.Item("@AgentID").DefaultValue =
    End Sub

    Protected Sub gvAgents_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles gvAgents.RowDeleting
        Me.dsAgents.DeleteParameters.Item("@AgentID").DefaultValue = Me.gvAgents.DataKeys(e.RowIndex).Value
    End Sub

    Protected Sub gvAgents_RowUpdating(sender As Object, e As GridViewUpdateEventArgs) Handles gvAgents.RowUpdating
        Me.dsAgents.UpdateParameters.Item("@AgentPic").DefaultValue = _
            CType(Me.gvAgents.Rows(e.RowIndex).FindControl("txtAgentPic"), WebControls.TextBox).Text
        Me.dsAgents.UpdateParameters.Item("@First_Name").DefaultValue = _
            CType(Me.gvAgents.Rows(e.RowIndex).FindControl("txtFirstName"), WebControls.TextBox).Text
        Me.dsAgents.UpdateParameters.Item("@Last_Name").DefaultValue = _
            CType(Me.gvAgents.Rows(e.RowIndex).FindControl("txtLastName"), WebControls.TextBox).Text
        Me.dsAgents.UpdateParameters.Item("@Address").DefaultValue = _
            CType(Me.gvAgents.Rows(e.RowIndex).FindControl("txtAddress"), WebControls.TextBox).Text
        Me.dsAgents.UpdateParameters.Item("@City").DefaultValue = _
            CType(Me.gvAgents.Rows(e.RowIndex).FindControl("txtCity"), WebControls.TextBox).Text
        Me.dsAgents.UpdateParameters.Item("@Phone_Number").DefaultValue = _
            CType(Me.gvAgents.Rows(e.RowIndex).FindControl("txtPhoneNumber"), WebControls.TextBox).Text
        Me.dsAgents.UpdateParameters.Item("@AgentID").DefaultValue = Me.gvAgents.DataKeys(e.RowIndex).Value
    End Sub
End Class
