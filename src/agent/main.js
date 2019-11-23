import App from './App.svelte';
import materialize from 'materialize-css';
import 'materialize-css/dist/css/materialize.min.css'; 

const app = new App({
	target: document.body,
	props: {
		name: 'world'
	}
});

materialize.AutoInit();

export default app;