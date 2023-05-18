import { Link } from 'react-router-dom';

type myButtonProps = {
  img: string;
  alt: string;
  url: string;
  style?: string;
};
export const ImageBtn = ({ img, alt, url, style }: myButtonProps) => {
  return (
    <Link to={url}>
      <img src={img} alt={alt} className={style} />
    </Link>
  );
};
