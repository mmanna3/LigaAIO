import { Link } from 'react-router-dom';

interface Props {
  content: string;
  path: string;
}

export const GenericButton = ({ path, content }: Props) => {
  return (
    <Link
      to={path}
      className='w-72 rounded-lg bg-title-darkGreen py-10 text-center text-white shadow-xl'
    >
      {content}
    </Link>
  );
};
