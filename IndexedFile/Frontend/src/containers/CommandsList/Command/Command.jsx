import React, { Component } from 'react';

import axios from '../../../axiosInstance';
import classes from './Command.scss';

export default class Command extends Component {
    constructor(props) {
        super(props);
        this.state = {
            value: '',
        };
    }

    onChange = event => {
        this.setState({
            value: event.target.value,
        });
    };

    onSubmit = event => {
        event.preventDefault();

        const { method, onUpdate } = this.props;
        const { value } = this.state;

        if (method === 'post') {
            axios.post('indexedfile', `value=${value}`).then(res => {
                onUpdate(res.data.data, res.data.indexes);
            });
        }

        if (method === 'delete') {
            axios.delete(`indexedfile/${value}`).then(res => {
                onUpdate(res.data.data, res.data.indexes);
            });
        }
    };

    render() {
        const { name, type } = this.props;

        return (
            <form
                className={classes.InputWrapper}
                method="POST"
                onSubmit={this.onSubmit}>
                <label htmlFor={name}>{name}</label>
                <input
                    type={type}
                    id={name}
                    name={name}
                    value={this.state.value}
                    onChange={this.onChange}
                />
                <button type="submit">Submit</button>
            </form>
        );
    }
}
