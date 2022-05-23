// Server modules
const ExpressServer = require('./core/expressServer');
const WsServer = require('./core/wsServer');

// Server instances
let expressServer = null;
let wsServer = null;

class AgarioServer {
    static runService() {
        // Create & Run express web server
        expressServer = new ExpressServer();
        const service = expressServer.run();

        // Attach express app to ws server
        wsServer = new WsServer(service);
        wsServer.run();
    }

    static test() {
        // Test code
        console.log('Test code');
    }
}

// Run server
AgarioServer.runService();
