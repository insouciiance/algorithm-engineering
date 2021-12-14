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

        const { onUpdate, onlyButton } = this.props;
        const { value } = this.state;

        if (onlyButton) {
            onUpdate();
        } else {
            onUpdate(value);
        }
    };

    render() {
        const { name, type, onlyButton } = this.props;
        const { value } = this.state;

        return (
            <form
                className={classes.InputWrapper}
                method="POST"
                onSubmit={this.onSubmit}>
                {!onlyButton ? (
                    <>
                        <label htmlFor={name}>{name}</label>
                        <input
                            type={type}
                            id={name}
                            name={name}
                            value={value}
                            onChange={this.onChange}
                        />
                    </>
                ) : null}
                <button type="submit">{onlyButton ? name : 'Submit'}</button>
            </form>
        );
    }
}
