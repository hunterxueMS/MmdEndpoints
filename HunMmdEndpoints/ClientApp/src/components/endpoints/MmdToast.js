import Toast from 'react-bootstrap/Toast';

export function MmdToast(props) {
    return (
        <Toast show={true} onClose={() => { props.onToastClose(); } }>
            <Toast.Header>
                <strong className="me-auto">{props.mainTitle}</strong>
                <small>{props.subTitle}</small>
            </Toast.Header>
            <Toast.Body>{props.message}</Toast.Body>
        </Toast>
    );
};
MmdToast.displayName = 'MmdToast';
