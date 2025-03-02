

Public Class clsCommonFunctions_BL


    'Author     :   Ashok R
    'Date       :   OCT 10,2013
    'About      :   Using this class user can Validate the Entering Character in a text box is number or 
    '               string 

    'In some situations the user only allowed to entre some digits 
    'inside the controls, no text is allowed inside the control.
    'This function allow the user to entre only numerical datas.

    'Returns    : Returns true if the digit is entred otherwise return false
    'parameter  : The key press event

    Public Shared Sub isNumeric(ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If e.KeyChar = "." Then Exit Sub

        If (e.KeyChar >= "!" And e.KeyChar <= "/") Or (e.KeyChar >= ":" And e.KeyChar <= "~") Then
            e.Handled = True
        End If

    End Sub

    Public Shared Sub isInteger(ByVal e As System.Windows.Forms.KeyPressEventArgs)

        If (e.KeyChar >= "!" And e.KeyChar <= "/") Or (e.KeyChar >= ":" And e.KeyChar <= "~") Then
            e.Handled = True

        End If

    End Sub
    'In some situation the user only allowed to entre string data inside
    'the controls, no digit values allowed , this function restrict 
    'user to entre the digit values.


    'Returns    : returns true if the string is entred
    'Parameter  :   the keypress event.
    Public Shared Sub isString(ByVal e As System.Windows.Forms.KeyPressEventArgs)

        If (e.KeyChar <= "!" And e.KeyChar >= "/") Or (e.KeyChar <= ":" And e.KeyChar >= "~") Then
            e.Handled = True
        End If
    End Sub
    Public Shared Sub SetGridHead(ByVal dgDataGrid As DataGridView)

        With dgDataGrid
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .AllowUserToAddRows = False
            .AllowUserToDeleteRows = False
            .AllowUserToResizeRows = False
            .AllowUserToResizeColumns = True
            .EditMode = DataGridViewEditMode.EditProgrammatically
            .ColumnHeadersDefaultCellStyle.BackColor = Color.WhiteSmoke
            .ColumnHeadersDefaultCellStyle.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular)
            .RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("Segoe UI", 8.5!)

        End With

    End Sub




    'Author     :   Ashok R
    'Date       :   Sept 16,2013
    'About      :   Common Usable Function. 
    '               This Function Helps us to Validate the mandatory fields are filled.
    '           :   If Not Filled .. Function force to fill that
    '           :   THID FUNCTION VALIDATE  ****One****  MANDATORY FIELDS
    '
    'Parameters 
    '   intCurrentRow >> Holds The Current Row Index 
    '   intCurrentColumn >> Holds The Current Column Index

    '   intMandatoryColumnOne >> Holds The  index of the first Mandatory Colum

    '   dataGridName >> Holds The datagridviewid(Name) 


    Public Shared Function isGridMandatoryFieldsFilled_One(ByVal intCurrentRow As Integer, ByVal intCurrentColumn As Integer, ByVal intMandatoryColumnOne As Integer, ByVal dataGridName As Windows.Forms.DataGridView) As Boolean

        Dim strMandatorycell1Value1 As String

        Dim Columncount As Integer

        Columncount = dataGridName.ColumnCount
        'Here it check the last column number with the total column
        'If the condition satisfy means the user hit enter for next row
        'before that we need to check all mandatory fields should filled
        If Columncount - 1 = intCurrentColumn Then

            strMandatorycell1Value1 = Trim(dataGridName.Rows(intCurrentRow).Cells(intMandatoryColumnOne).FormattedValue.ToString())

            'if both conditions are satisfied,that means The user entered Mandatory Fields 
            If strMandatorycell1Value1 <> "" Then
                Return True
            Else
                MessageBox.Show("Should Complete the Mandatory Items ")
                Return False
                Exit Function

            End If

        End If
    End Function
    'Author     :   Ashok R
    'Date       :   Sept 16,2013
    'About      :   Common Usable Function. 
    '               This Function Helps us to Validate the mandatory fields are filled.
    '           :   If Not Filled .. Function force to fill that
    '           :   THID FUNCTION VALIDATE  ****TWO****  MANDATORY FIELDS
    '
    'Parameters 
    '   intCurrentRow >> Holds The Current Row Index 
    '   intCurrentColumn >> Holds The Current Column Index

    '   intMandatoryColumnOne >> Holds The  index of the first Mandatory Colum
    '   intMandatoryColumnTwo >> Holds The  index of the Second Mandatory Colum

    '   dataGridName >> Holds The datagridviewid(Name) 


    Public Shared Function isGridMandatoryFieldsFilled_Two(ByVal intCurrentRow As Integer, ByVal intCurrentColumn As Integer, ByVal intMandatoryColumnOne As Integer, ByVal intMandatoryColumnTwo As Integer, ByVal dataGridName As Windows.Forms.DataGridView) As Integer

        Dim strMandatorycellValue1 As String
        Dim strMandatorycellValue2 As String

        Dim Columncount As Integer
        Dim intReturnSuccess As Integer = 0

        Columncount = dataGridName.ColumnCount
        'Here it check the last column number with the total column
        'If the condition satisfy means the user hit enter for next row
        'before that we need to check all mandatory fields should filled
        If Columncount - 1 = intCurrentColumn Then

            strMandatorycellValue1 = Trim(dataGridName.Rows(intCurrentRow).Cells(intMandatoryColumnOne).FormattedValue.ToString())
            strMandatorycellValue2 = Trim(dataGridName.Rows(intCurrentRow).Cells(intMandatoryColumnTwo).FormattedValue.ToString())

            'if both conditions are satisfied,that means The user entered Mandatory Fields 
            If strMandatorycellValue1 = Nothing Then

                MessageBox.Show("Should Complete the Mandatory Items ")
                dataGridName.CurrentCell = dataGridName.Item(intCurrentColumn, intCurrentRow)
                Return intCurrentColumn
            ElseIf strMandatorycellValue2 = Nothing Then
                MessageBox.Show("Should Complete the Mandatory Items ")
                'frmFormName.Controls(dataGridName).CurrentCell = dataGridName.Item(intCurrentColumn, intCurrentRow)

                dataGridName.CurrentCell = dataGridName.Item(intCurrentColumn, intCurrentRow)
                Return intCurrentColumn

            Else

                Return intReturnSuccess
                Exit Function
            End If
        End If

    End Function
    'Author     :   Ashok R
    'Date       :   Sept 16,2013
    'About      :   Common Usable Function. 
    '               This Function Helps us to Validate the mandatory fields are filled.
    '           :   If Not Filled .. Function force to fill that
    '           :   THID FUNCTION VALIDATE  ****THREE****  MANDATORY FIELDS
    '
    'Parameters 
    '   intCurrentRow >> Holds The Current Row Index 
    '   intCurrentColumn >> Holds The Current Column Index

    '   intMandatoryColumnOne >> Holds The  index of the first Mandatory Colum
    '   intMandatoryColumnTwo >> Holds The  index of the Second Mandatory Colum
    '   intMandatoryColumnThree >> Holds The  index of the Third Mandatory Colum

    '   dataGridName >> Holds The datagridviewid(Name) 


    Public Shared Function isGridMandatoryFieldsFilled_Two(ByVal intCurrentRow As Integer, ByVal intCurrentColumn As Integer, ByVal intMandatoryColumnOne As Integer, ByVal intMandatoryColumnTwo As Integer, ByVal intMandatoryColumnThree As Integer, ByVal dataGridName As Windows.Forms.DataGridView) As Boolean

        Dim strMandatorycell1Value1 As String
        Dim strMandatorycell1Value2 As String
        Dim strMandatorycell1Value3 As String

        Dim Columncount As Integer

        Columncount = dataGridName.ColumnCount
        'Here it check the last column number with the total column
        'If the condition satisfy means the user hit enter for next row
        'before that we need to check all mandatory fields should filled
        If Columncount - 1 = intCurrentColumn Then

            strMandatorycell1Value1 = Trim(dataGridName.Rows(intCurrentRow).Cells(intMandatoryColumnOne).FormattedValue.ToString())
            strMandatorycell1Value2 = Trim(dataGridName.Rows(intCurrentRow).Cells(intMandatoryColumnTwo).FormattedValue.ToString())
            strMandatorycell1Value3 = Trim(dataGridName.Rows(intCurrentRow).Cells(intMandatoryColumnThree).FormattedValue.ToString())

            'if both conditions are satisfied,that means The user entered Mandatory Fields 
            If strMandatorycell1Value1 <> "" And strMandatorycell1Value2 <> "" And strMandatorycell1Value3 <> "" Then
                Return True
            Else
                MessageBox.Show("Should Complete the Mandatory Items ")
                Return False
                Exit Function

            End If

        End If
    End Function
    'Author     :   Ashok R
    'Date       :   Sept 16,2013
    'About      :   Common Usable Function
    '               Using this class user can Validate an allready entered datagrid items with New entry .
    '               using this function User can evaluate ***TWO*** fields in data grid.
    '           :   Function returns boolean value
    '
    'Parameters 
    '   lstrNewEnteredItemOne >> Holds The first string value which we want to add data grid
    '   lstrNewEnteredItemTwo >> Holds The second string value which we want to add data grid

    '   lintItemColumnIndexOne >> Holds The Column index of the data grid which we want to add first string
    '   lintItemColumnIndexTwo >> Holds The Column index of the data grid which we want to add second string

    '   dataGridId >> Holds The datagridviewid(Name) 


    Public Shared Function isGridItemsRepeated(ByVal lstrNewEnteredItemOne As String, ByVal lstrNewEnteredItemTwo As String, ByVal lintItemColumnIndexOne As Integer, ByVal lintItemColumnIndexTwo As Integer, ByVal dataGridId As Windows.Forms.DataGridView) As Boolean

        Dim Rowcount As Integer
        Dim lstrFirstItem As String
        Dim lstrSecondItem As String

        If dataGridId.Rows.Count > 0 Then

            'we must check from the first row to last row for find out the new item is already exist 
            'or not in any row of data grid
            For Rowcount = 0 To dataGridId.Rows.Count - 2
                'take the items from the grid 
                lstrFirstItem = Trim(dataGridId.Item(lintItemColumnIndexOne, Rowcount).Value)
                lstrSecondItem = Val(dataGridId.Item(lintItemColumnIndexTwo, Rowcount).Value)
                'Comparing grid item with new entered items
                'if both conditions are satisfied,that means thitr is already a row of entry with 
                'the same item which is currently entered
                If lstrFirstItem = "" And lstrSecondItem = "" Then
                    ''Removes the row that already entered 
                    'dataGridId.Rows.Remove(dataGridId.Rows(Rowcount))
                    MessageBox.Show("Should Complete the Mandatory Items ")
                    Return True
                    Exit Function
                End If
            Next
        End If
    End Function
    'Author     :   Ashok R
    'Date       :   March 19,2009
    'About      :   Common Usable Function
    '               Using this class user can Validate an allready entered datagrid items with New entry .
    '               using this function User can evaluate ***THREE*** fields in data grid.
    '           :   Function returns boolean value
    '
    'Parameters 
    '   lstrNewEnteredItemOne >> Holds The first string value which we want to add data grid
    '   lstrNewEnteredItemTwo >> Holds The second string value which we want to add data grid
    '   lstrNewEnteredItemThree >> Holds The Third string value which we want to add data grid

    '   lintItemColumnIndexOne >> Holds The Column index of the data grid which we want to add first string
    '   lintItemColumnIndexTwo >> Holds The Column index of the data grid which we want to add second string
    '   lintItemColumnIndexThree >> Holds The Column index of the data grid which we want to add third string

    '   dataGridId >> Holds The datagridviewid(Name) 
    '   nextFocousedControlId >> Holds The Id(Name) of the control when the focus goes to


    Public Shared Function isGridItemsRepeated(ByVal lstrNewEnteredItemOne As String, ByVal lstrNewEnteredItemTwo As String, ByVal lstrNewEnteredItemThree As String, ByVal lintItemColumnIndexOne As Integer, ByVal lintItemColumnIndexTwo As Integer, ByVal lintItemColumnIndeThree As Integer, ByVal lintItemQuantityIndex As Integer, ByVal QuantityTextBoxId As Windows.Forms.TextBox, ByVal dataGridId As Windows.Forms.DataGridView) As Boolean

        Dim Rowcount As Integer
        Dim lstrFirstItem As String
        Dim lstrSecondItem As String
        Dim lstrThirdItem As String

        If dataGridId.Rows.Count > 0 Then

            'we must check from the first row to last row for find out the new item is already exist 
            'or not in any row of data grid
            For Rowcount = 0 To dataGridId.Rows.Count - 2
                'take the items from the grid 
                lstrFirstItem = Trim(dataGridId.Item(lintItemColumnIndexOne, Rowcount).Value)
                lstrSecondItem = Val(dataGridId.Item(lintItemColumnIndexTwo, Rowcount).Value)
                lstrThirdItem = dataGridId.Item(lintItemColumnIndeThree, Rowcount).Value
                'Comparing grid item with new entered items
                'if both conditions are satisfied,that means thitr is already a row of entry with 
                'the same item which is currently entered
                If lstrFirstItem = Trim(lstrNewEnteredItemOne) And lstrSecondItem = Val(lstrNewEnteredItemTwo) And lstrThirdItem = Trim(lstrNewEnteredItemThree) Then

                    QuantityTextBoxId.Text = QuantityTextBoxId.Text + Val(dataGridId.Item(lintItemQuantityIndex, Rowcount).Value)
                    'ShowMessageBox.toGeneralInformation("Item Already Exist")
                    'Removes the row that already entered 
                    dataGridId.Rows.Remove(dataGridId.Rows(Rowcount))
                    'nextFocousedControlId.Focus()
                    Return True
                    Exit Function
                End If
            Next
        End If
    End Function
    'Author     :   Ashok R
    'Date       :   March 19,2009
    'About      :   Common Usable Function
    '               Using this class user can Validate an allready entered datagrid items with New entry .
    '               using this function User can evaluate ***THREE*** fields in data grid.
    '           :   Function returns boolean value
    '
    'Parameters 
    '   lstrNewEnteredItemOne >> Holds The first string value which we want to add data grid
    '   lstrNewEnteredItemTwo >> Holds The second string value which we want to add data grid
    '   lstrNewEnteredItemThree >> Holds The Third string value which we want to add data grid

    '   lintItemColumnIndexOne >> Holds The Column index of the data grid which we want to add first string
    '   lintItemColumnIndexTwo >> Holds The Column index of the data grid which we want to add second string
    '   lintItemColumnIndexThree >> Holds The Column index of the data grid which we want to add third string

    '   dataGridId >> Holds The datagridviewid(Name) 
    '   nextFocousedControlId >> Holds The Id(Name) of the control when the focus goes to


    Public Shared Function isGridItemsRepeated(ByVal lstrNewEnteredItemOne As String, ByVal lstrNewEnteredItemTwo As String, ByVal lstrNewEnteredItemThree As String, ByVal lintItemColumnIndexOne As Integer, ByVal lintItemColumnIndexTwo As Integer, ByVal lintItemColumnIndeThree As Integer, ByVal lintAmountIndex As Integer, ByVal dataGridId As Windows.Forms.DataGridView, ByVal AmountTextBoxId As Windows.Forms.TextBox) As Boolean
        Dim Rowcount As Integer
        Dim lstrFirstItem As String
        Dim lstrSecondItem As String
        Dim lstrThirdItem As String
        '
        If dataGridId.Rows.Count > 0 Then
            'we must check from the first row to last row for find out the new item is already exist 
            'or not in any row of data grid
            For Rowcount = 0 To dataGridId.Rows.Count - 2
                'take the items from the grid 
                lstrFirstItem = Trim(dataGridId.Item(lintItemColumnIndexOne, Rowcount).Value)
                lstrSecondItem = Trim(dataGridId.Item(lintItemColumnIndexTwo, Rowcount).Value)
                lstrThirdItem = Trim(dataGridId.Item(lintItemColumnIndeThree, Rowcount).Value)
                'Comparing grid item with new entered items
                'if both conditions are satisfied,that means thitr is already a row of entry with 
                'the same item which is currently entered
                If lstrFirstItem = Trim(lstrNewEnteredItemOne) And lstrSecondItem = Trim(lstrNewEnteredItemTwo) And lstrThirdItem = Trim(lstrNewEnteredItemThree) Then

                    AmountTextBoxId.Text = AmountTextBoxId.Text + Val(dataGridId.Item(lintAmountIndex, Rowcount).Value)
                    'ShowMessageBox.toGeneralInformation("Item Already Exist")
                    'Removes the row that already entered 
                    dataGridId.Rows.Remove(dataGridId.Rows(Rowcount))
                    'nextFocousedControlId.Focus()
                    Return True
                    Exit Function
                End If
            Next
        End If
    End Function
    'Author     :   Ashok R
    'Date       :   March 19,2009
    'About      :   Common Usable Function
    '               Using this class user can Validate an allready entered datagrid items with New entry .
    '               using this function User can evaluate ***TWO*** fields in data grid.
    '           :   Function returns boolean value
    '
    'Parameters 
    '   lstrNewEnteredItemOne >> Holds The first string value which we want to add data grid
    '   lstrNewEnteredItemTwo >> Holds The second string value which we want to add data grid

    '   lintItemColumnIndexOne >> Holds The Column index of the data grid which we want to add first string
    '   lintItemColumnIndexTwo >> Holds The Column index of the data grid which we want to add second string

    '   dataGridId >> Holds The datagridviewid(Name) 
    '   nextFocousedControlId >> Holds The Id(Name) of the control when the focus goes to


    Public Shared Function isGridItemsRepeated(ByVal lstrNewEnteredItemOne As String, ByVal lstrNewEnteredItemTwo As String, ByVal lintItemColumnIndexOne As Integer, ByVal lintItemColumnIndexTwo As Integer, ByVal dataGridId As Windows.Forms.DataGridView, ByVal NextFocusedControl As Windows.Forms.Control) As Boolean
        'Public Shared Function isGridItemsRepeated(ByVal lstrNewEnteredItemOne As String, ByVal lstrNewEnteredItemTwo As String, ByVal lintItemColumnIndexOne As Integer, ByVal lintItemColumnIndexTwo As Integer, ByVal dataGridId As Windows.Forms.DataGridView, ByVal nextFocousedControlId As Windows.Forms.Control) As Boolean

        Dim Rowcount As Integer
        Dim lstrFirstItem As String
        Dim lstrSecondItem As String

        If dataGridId.Rows.Count > 0 Then

            'we must check from the first row to last row for find out the new item is already exist 
            'or not in any row of data grid
            For Rowcount = 0 To dataGridId.Rows.Count - 2
                'take the items from the grid 
                lstrFirstItem = Trim(dataGridId.Item(lintItemColumnIndexOne, Rowcount).Value)
                lstrSecondItem = Trim(dataGridId.Item(lintItemColumnIndexTwo, Rowcount).Value)
                'Comparing grid item with new entered items
                'if both conditions are satisfied,that means thitr is already a row of entry with 
                'the same item which is currently entered
                If lstrFirstItem.ToUpper = Trim(lstrNewEnteredItemOne).ToUpper And lstrSecondItem.ToUpper = (lstrNewEnteredItemTwo).ToUpper Then
                    'ShowMessageBox.toGeneralInformation("Item Already Exist")
                    MessageBox.Show("Item Already Exist")

                    ''Removes the row that already entered 
                    'dataGridId.Rows.Remove(dataGridId.Rows(Rowcount))
                    NextFocusedControl.Focus()
                    Return True
                    Exit Function
                End If
            Next
        End If
    End Function
    'Check if an item is repeated in the grid
    Public Shared Function isGridItemsRepeated(ByVal ItemTextBox As Windows.Forms.TextBox, ByVal dataGridId As Windows.Forms.DataGridView, ByVal GridColumnNo As Integer) As Boolean
        Dim rowcount As Integer
        Dim lstrGridValue As String

        If dataGridId.Rows.Count > 1 Then
            For rowcount = 0 To dataGridId.Rows.Count - 2
                lstrGridValue = dataGridId.Item(GridColumnNo, rowcount).Value
                If Trim(ItemTextBox.Text).ToUpper = lstrGridValue.ToUpper Then
                    'ShowMessageBox.toGeneralInformation("Item Already Exist In The List")
                    MessageBox.Show("Item Already Exist In The List")
                    ItemTextBox.Focus()
                    Return False
                End If
            Next
            Return True
        Else
            Return True
        End If

    End Function

End Class
