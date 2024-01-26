import React, { Component } from 'react';
import authService from '../../components/api-authorization/AuthorizeService';

export class EmployeeCalculate extends Component {
  static displayName = EmployeeCalculate.name;

  constructor(props) {
    super(props);
      this.state = { id: 0, fullName: '', birthdate: '', tin: '', typeId: 1, absentDays: 0, workedDays: 0, netIncome: 0, loading: true, loadingCalculate: false };
  }

  componentDidMount() {
    this.getEmployee(this.props.match.params.id);
  }
  handleChange(event) {
    this.setState({ [event.target.name] : event.target.value});
  }

  handleSubmit(e){
      e.preventDefault();
      this.calculateSalary();
  }

  render() {

    let contents = this.state.loading
    ? <p><em>Loading...</em></p>
    : <div>
    <form>
<div className='form-row'>
<div className='form-group col-md-12'>
  <label>Full Name: <b>{this.state.fullName}</b></label>
</div>

</div>

<div className='form-row'>
<div className='form-group col-md-12'>
  <label >Birthdate: <b>{this.state.birthdate}</b></label>
</div>
</div>

<div className="form-row">
<div className='form-group col-md-12'>
  <label>TIN: <b>{this.state.tin}</b></label>
</div>
</div>

<div className="form-row">
<div className='form-group col-md-12'>
  <label>Employee Type: <b>{this.state.employeeType}</b></label>
</div>
</div>

                <div className="form-row" id="salaryDetails">
                    <div className='form-group col-md-12'><label>{this.state.salary}</label></div>
                    <div className='form-group col-md-12' hidden={this.state.isTaxHidden}><label>{this.state.tax}</label></div>                          
</div>
<div className="form-row">
                    <div className='form-group col-md-6'>
                        <label htmlFor='inputAbsentDays4'>{this.state.inputDaysLabel} </label>
                        <input type='text' className='form-control' id="inputDays" onChange={this.handleChange.bind(this)} value={this.state.inputDays} name="inputDays" placeholder={this.state.inputDaysPlace} required/>
                    </div>
</div>

<div className="form-row">
<div className='form-group col-md-12'>
  <label>Net Income: <b>{this.state.netIncome}</b></label>
</div>
</div>

<button type="submit" onClick={this.handleSubmit.bind(this)} disabled={this.state.loadingCalculate} className="btn btn-primary mr-2">{this.state.loadingCalculate?"Loading...": "Calculate"}</button>
<button type="button" onClick={() => this.props.history.push("/employees/index")} className="btn btn-primary">Back</button>
</form>
</div>;


    return (
        <div>
        <h1 id="tabelLabel" >Employee Calculate Salary</h1>
        <br/>
        {contents}
      </div>
    );
  }

  async calculateSalary() {
    this.setState({ loadingCalculate: true });
    const token = await authService.getAccessToken();
    const requestOptions = {
        method: 'POST',
        headers: !token ? {} : { 'Authorization': `Bearer ${token}`, 'Content-Type': 'application/json' },
        body: JSON.stringify({ id: this.state.id, inputDays: this.state.inputDays, employeeTypeId: this.state.employeeTypeId })
    }; 
      const response = await fetch('api/employees/calculate', requestOptions); 
      const data = await response.json();
      if (response.status === 200 && data.message != null) {
          alert(data.message);
      }
      else if (response.status !== 200) {
          alert("There was an error occured.")
      }
    this.setState({ loadingCalculate: false, netIncome: data.salary });
  }

  async getEmployee(id) {
    this.setState({ loading: true,loadingCalculate: false });
    const token = await authService.getAccessToken();
    const response = await fetch('api/employees/' + id, {
      headers: !token ? {} : { 'Authorization': `Bearer ${token}` }
    });

    const data = await response.json()
    if(response.status === 200){;
        this.setState({ id: data.id, fullName: data.fullName, birthdate: data.birthdate, tin: data.tin, employeeTypeId: data.employeeTypeId, employeeType: data.employeeType, loading: false,loadingCalculate: false });
    }
    else{
        alert("There was an error occured.");
        this.setState({ loading: false,loadingCalculate: false });
      }
       
      if (data.employeeTypeId === 1) {
          this.setState({ tax: "Tax: 12%", salary: "Salary: 20000", inputDaysLabel: "Absent Days:", inputDaysId: "inputAbsentDays4", inputDaysName: "absentDays", inputDaysPlace: "Absent Days", inputDays: "", isTaxHidden: false });
          
      }
      else if (data.employeeTypeId === 2) {

          this.setState({ tax: "", salary: "Rate Per Day: 500", inputDaysLabel: "Worked Days: ", inputDaysId: "inputAbsentDays4", inputDaysName: "workedDays", inputDaysPlace: "Worked Days", inputDays: "", isTaxHidden: true });
      } 
      if (data.employeeTypeId === 3) {
          this.setState({ tax: "Tax: 12%", salary: "Salary: 16000", inputDaysLabel: "Absent Days:", inputDaysId: "inputAbsentDays4", inputDaysName: "absentDays", inputDaysPlace: "Absent Days", inputDays: "", isTaxHidden: false });

      }
      else if (data.employeeTypeId === 4) {

          this.setState({ tax: "", salary: "Rate Per Day: 400", inputDaysLabel: "Worked Days: ", inputDaysId: "inputAbsentDays4", inputDaysName: "workedDays", inputDaysPlace: "Worked Days", inputDays: "", isTaxHidden: true });
      } 
      
  }
}