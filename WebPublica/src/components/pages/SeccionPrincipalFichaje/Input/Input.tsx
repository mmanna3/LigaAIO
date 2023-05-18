import React, { useState } from 'react';
import styles from './Input.module.css';
// import bootstrap from "GlobalStyle/bootstrap.min.css";

interface IInput {
  onChange?: (value: string) => void;
  name?: string;
  register?: any;
  type: string;
  className?: string;
}

const Input = ({ onChange, name, register, type = 'text', className }: IInput) => {
  const [valor, setValor] = useState('');

  const handleOnChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    if (onChange) onChange(e.target.value);
    setValor(e.target.value);
  };

  return (
    <input
      {...register}
      name={name}
      className={styles.input + ' text-black' + ' ' + className}
      value={valor}
      type={type}
      onChange={handleOnChange}
      autoComplete='off'
    />
  );
};

export default Input;
