pipeline {
    agent any

    environment {
        CURRENT_PATH = sh(script: "pwd", returnStdout: true).trim()
        PATH_DOCKER_IMAGES = "/root/swan/images"
    }

    stages {
        stage('Check docker') {
            steps {
	        sh "docker --version"
	    }
        }
        // SERVER
        stage('Build server image') {
            steps {
                sh '''
		    docker build -t swan_server ./Server
		    docker container prune -f
		    docker image prune -f
		'''
            }
        }
        stage('Save server image') {
            steps {
                sh "docker save -o ${env.PATH_DOCKER_IMAGES}/swan_server.tar swan_server"
            }
        }
        // CLIENT
        stage('Build client image') {
            steps {
                sh '''
                    docker build -t swan_client ./Client/react-client
		    docker container prune -f
		    docker image prune -f
                '''
            }
        }
        stage('Save client image') {
            steps {
                sh "docker save -o ${env.PATH_DOCKER_IMAGES}/swan_client.tar swan_client"
            }
        }
    }
}
